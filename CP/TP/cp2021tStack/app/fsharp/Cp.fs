// (c) MP-I (1998/9-2006/7) and CP (2005/6-2020/21)

module Cp

// (0) Basics

let succ = (+) 1

let konst x _ = x

// (1) Product

// Renamings

let p1 = fst
let p2 = snd

let split f g x = (f x, g x)

let (><) f g = split (f << p1) (g << p2)

// the 0-adic split 

let (!) x = konst () x

// diagonal

let diag x = split id id x

// (2) Coproduct

type Either<'a,'b> = Left of 'a | Right of 'b

// Renamings

let i1 = Left
let i2 = Right

let either f g x =
    match x with
    | Left a -> f a
    | Right b -> g b

let (-|-) f g = either (i1 << f) (i2 << g)

// McCarthy's conditional:

let grd p x = if p x then Left x else Right x

let cond p f g = (either f g) << (grd p)

// codiagonal

let codiag x = either id id x

// (3) Exponentiation ---------------------------------------------------------

let curry f a b = f (a,b)

let uncurry f (a,b) = f a b

let ap (f,x) = f x

let expn f = curry (f << ap)

let flip f = fun x y -> f y x

let p2p p b = if b then (snd p) else (fst p) // pair to predicate

// (5) Natural isomorphisms ----------------------------------------------------

let swap x = split p2 p1 x

let assocr x = split ( p1 << p1 ) (p2 >< id) x

let assocl x = split ( id >< p1 ) ( p2 >> p2 ) x

let undistr x = either ( id >< Left ) ( id >< Right ) x

let undistl x = either ( i1 >< id ) ( i2 >< id ) x

let flatr (a,(b,c)) = (a,b,c)

let flatl ((b,c),d) = (b,c,d)

let br x = split id (!) x // 'bang' on the right

let bl x = (swap << br) x // 'bang' on the left

let coswap x = either i2 i1 x

let coassocr x = either (id -|- i1) (i2 << i2) x

let coassocl x = either (i1 << i1) (i2 -|- id) x

let distl x = uncurry (either (curry i1) (curry i2)) x

let distr x = ((swap -|- swap) << distl << swap) x

let colambda (a,b) = fun f x ->
    match x with
    | true -> a
    | false -> b

let lambda f = (f false, f true)

// (6) Class bifunctor ---------------------------------------------------------
(*
type BiFunctor<'f> =
    abstract member bmap : (a -> b) -> (c -> d) -> ('f a c -> 'f b d)

interface BiFunctor<Either> with
    member bmap f g = f -|- g

interface BiFunctor<(,)> with
    member bmap f g  = f >< g
*)
// (8) Basic functions, abbreviations ------------------------------------------

let bang = (!)

let dup x = split id id x

let zero x = konst 0 x

let one x = konst 1 x

let nil x = konst [] x

//let cons = uncurry (::)

let add = uncurry (+)

let mul = uncurry (*)

//let conc = uncurry (++)

//let true = const True

let nothing x = konst None x

//let false = const False

//inMaybe :: Either () a -> Maybe a
let inMaybe x = either (konst None) Some x

// testing

let alfa x = 
    match x with
    | Left a -> (Left << Left) a
    | Right (Left a) -> (Left << Right) a
    | Right (Right a) -> Right a

let alfa' x = either (i1 << i1) (i2 -|- id) x

let alfa'' x = ((id -|- p1) << i2 << p2) x