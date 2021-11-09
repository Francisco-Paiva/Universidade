// (c) MP-I (1998/9-2006/7) and CP (2005/6-2020/21)

module Nat

open Cp

// (1) Datatype definition -----------------------------------------------------

// "data Nat = 0 | succ Nat"   -- in fact: Haskell Integer is used as carrier type

let inNat x = either (konst 0) succ x

let outNat n =
    match n with
    | 0 -> i1 ()
    | _ -> i2 (n-1)

// NB: inNat and outNat are isomorphisms only if restricted to non-negative integers

// (2) Ana + cata + hylo -------------------------------------------------------

let recNat f x = (id -|- f) x                                 // this is F f for this datatype

let rec cataNat g x = (g << recNat (cataNat g) << outNat) x

let rec anaNat h x = (inNat << (recNat (anaNat h) ) << h) x

let hyloNat g h x = (cataNat g << anaNat h) x

let rec paraNat g x = (g << recNat (split id (paraNat g)) << outNat) x

// (3) Map ---------------------------------------------------------------------

// (4) Examples ----------------------------------------------------------------

// (4.1) for is the "fold" of natural numbers

let for' b i = cataNat (either (konst i) b)

let somar a = cataNat (either (konst a) succ)      // for succ a

let multip a = cataNat (either (konst 0) ((+) a))   // for (a+) 0

let exp a = cataNat (either (konst 1) ((*) a))      // for (a*) 1

// (4.2) sq (square of a natural number) 

let rec sq n =
    match n with
    | 0 -> 0
    | _ -> let oddn n = 2*n+1 in oddn n + sq (n-1)

// sq' = paraNat (either (const 0) g)i

let sq' =
    // this is the outcome of calculating sq as a for loop using the
    // mutual recursion law
    let aux = cataNat (either (split (konst 0) (konst 1)) (split (uncurry (+)) (((+) 2) << p2)))
    in p1 << aux

let sq'' n  = // the same as a for loop (putting variables in)
    let body(s,o) = (s+o,2+o)
    in p1 (for' body (0,1) n)

// (4.3) factorial

let fac =
    let facfor = for' (split (succ << p1) mul) (1,1) 
    in p2 << facfor

let factorial =
    let g(n,r) = (n+1) * r
    in paraNat (either (konst 1) g)

// (4.4) integer division as an anamorphism --------------
(*
idiv :: Integer -> Integer -> Integer
{-- pointwise
x `idiv` y  |   x <  y    = 0
            |   x >= y    = (x - y) `idiv` y + 1
--}

idiv = flip aux

aux y = anaNat divide where 
           divide x | x <  y  = i1 ()
                    | x >= y  = i2 (x - y)
*)
// (4.5) bubble sort -----------------------------------

let rec bubble l =
    match l with
    | (x::y::xs) -> if x > y then y :: bubble (x :: xs) else x :: bubble (y :: xs)
    | x -> x

let bSort xs = for' bubble xs (List.length xs)

// (5) While loop -------------------------------------

let rec while' p f x = if not (p x) then x else while' p f (f x)

//while :: (a -> Bool) -> (a -> a) -> a -> a
//while p f = w where w =  (either id id) . (w -|- id) . (f -|- id) . (grd p)

// (5) Monadic for -------------------------------------

//mfor b i 0 = i
//mfor b i (n+1) = do {x <- mfor b i n ; b x}

// end of Nat.hs ----------------------------------------