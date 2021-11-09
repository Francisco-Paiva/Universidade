
// (c) MP-I (1998/9-2006/7) and CP (2005/6-2016/7)

module LTree

open Cp
//import Data.Monoid
//import Control.Applicative
//import List

// (1) Datatype definition -----------------------------------------------------

type LTree<'a> = Leaf of 'a | Fork of LTree<'a> * LTree<'a>

let inLTree x = either Leaf Fork x

let outLTree x =
     match x with
     | Leaf a -> i1 a
     | Fork (t1,t2) -> i2 (t1,t2)

// (2) Ana + cata + hylo -------------------------------------------------------

let baseLTree g f = g -|- (f >< f)

let recLTree f = baseLTree id f          // that is:  id -|- (f >< f)

let rec cataLTree a = a << (recLTree (cataLTree a)) << outLTree

let rec anaLTree f = inLTree << (recLTree (anaLTree f) ) << f

let hyloLTree a c = cataLTree a << anaLTree c

// (3) Map ---------------------------------------------------------------------

//instance Functor LTree
//         where fmap f = cataLTree ( inLTree . baseLTree f id )
let fmap f = cataLTree ( inLTree << baseLTree f id )

// (4) Examples ----------------------------------------------------------------

// (4.0) Inversion (mirror) ----------------------------------------------------

let invLTree x = cataLTree (inLTree << (id -|- swap)) x

(* Recall the pointwise version:
invLTree (Leaf a) = Leaf a
invLTree (Fork (a,b)) = Fork (invLTree b,invLTree a)
*)

// (4.1) Counting --------------------------------------------------------------

let countLTree x = cataLTree (either one add) x

// (4.2) Serialization ---------------------------------------------------------
let singl x = [x]

let tips x =
     let conc(l,r)= l @ r
     in cataLTree (either singl conc) x

// (4.3) Double factorial ------------------------------------------------------

let dfacd (n,m) = if n = m then i1 n else let k = (n+m) / 2 in i2 ((n,k),(k+1,m))

let dfac n =
     match n with
     | 0 -> 1
     | _ -> let mul(x,y) = x*y in hyloLTree (either id mul) dfacd (1,n)

// (4.4) Double square function ------------------------------------------------

// recall sq' in RList.hs in...
let add(x,y) = x+y

let dsq' n =
     match n with
     | 0 -> 0
     | _ -> (cataLTree (either id add) << fmap (fun n -> 2*n-1) << (anaLTree dfacd)) (1,n)

let dsq n =
     match n with
     | 0 -> 0
     | otherwise -> 
          let nthodd n = 2*n - 1
          let fdfacd f (n,m) = if n = m then i1 (f n) else let k = (n+m) / 2 in i2 ((n,k),(k+1,m))
          (hyloLTree (either id add) (fdfacd nthodd)) (1,n)

// (4.5) Fibonacci -------------------------------------------------------------

let fibd n = if n < 2 then i1 () else i2 (n-1, n-2)

let fib =  hyloLTree (either one add) fibd

// (4.6) Merge sort ------------------------------------------------------------

// singl   x = [x]

let rec merge (l,r) =
     match (l,r) with
     | (_,[]) -> l
     | ([],_) -> r
     | (x::xs,y::ys) -> if x < y then x :: merge (xs, y :: ys) else y :: merge (x :: xs, ys)

let rec sep l =
     match l with
     | [] -> ([],[])
     | (h::t) -> let (l,r) = sep t in (h::r,l)  // a List cata

let lsplit l =
     match l with
     | [x] -> i1 x
     | otherwise -> i2 (sep l)

let mSort' l =
     match l with
     | [] -> []
     | otherwise -> hyloLTree (either singl merge) lsplit l

// pointwise version:

let rec mSort l =
     match l with
     | [] -> []
     | [x] -> [x]
     | otherwise -> let (l1,l2) = sep l in merge(mSort l1, mSort l2)

// (4.7) Double map (unordered lists) ------------------------------------------

let conc(l,r)= l @ r

let dmap f x =
     if x = [] then []
     else hyloLTree (either (singl << f) conc) lsplit x

// (4.8) Double map (keeps the order) ------------------------------------------

let drop (m:int) (l:'a list) = l.[m..]

let divide l =
     match l with
     | [x] -> i1 x
     | otherwise -> let m = (List.length l) / 2 in i2 (split (List.take m) (drop m) l)

let dmap1 f x =
     if x = [] then []
     else (hyloLTree (either (singl << f) conc) divide) x
(*
-- (5) Monad -------------------------------------------------------------------

instance Monad LTree where
     return  = Leaf
     t >>= g = (mu . fmap g) t

instance Strong LTree

mu  :: LTree (LTree a) -> LTree a
mu  =  cataLTree (either id Fork)

{-- fmap :: (Monad m) => (t -> a) -> m t -> m a
    fmap f t = do { a <- t ; return (f a) }
--}

-- (6) Going polytipic -------------------------------------------------------

-- natural transformation from base functor to monoid
tnat :: Monoid c => (a -> c) -> Either a (c, c) -> c
tnat f = either f (uncurry mappend)

-- monoid reduction 

monLTree f = cataLTree (tnat f)

-- alternative to (4.2) serialization ----------------------------------------

tips' = monLTree singl

-- alternative to (4.1) counting ---------------------------------------------

countLTree' = monLTree (const (Sum 1))

-- distributive law ----------------------------------------------------------

dlLTree :: Strong f => LTree (f a) -> f (LTree a)
dlLTree = cataLTree (either (fmap Leaf) (fmap Fork .dstr))

-- (7) Zipper ----------------------------------------------------------------

data Deriv a = Dr Bool (LTree a)

type Zipper a = [ Deriv a ]

plug :: Zipper a -> LTree a -> LTree a
plug [] t = t
plug ((Dr False l):z) t = Fork (plug z t,l) 
plug ((Dr True  r):z) t = Fork (r,plug z t)

-- (8) Advanced --------------------------------------------------------------

instance Applicative LTree where
    pure = return
    (<*>) = aap

---------------------------- end of library ----------------------------------
*)