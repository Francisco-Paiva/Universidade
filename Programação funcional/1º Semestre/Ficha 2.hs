dobros :: [Float] -> [Float]
dobros [] = []
dobros (h:t) = 2 * h : dobros t

--numOcorre :: Char -> String -> Int
--numOcorre c [] = 0
--numOcorre c (h:t) = if c == h then 1+numOcorre c t
--                    else numOcorre c t

positivos :: [Int] -> Bool
positivos [] = False
positivos [x] = if x > 0 then True
                else False
positivos (h:t) |h > 0 = positivos t
                |h <= 0 = False

soPos :: [Int] -> [Int]
soPos [] = []
soPos (h:t) |h > 0 = h:soPos t
            |h <= 0 = soPos t

somaNeg :: [Int] -> Int
somaNeg [] = 0
somaNeg (h:t) |h < 0 = h + somaNeg t
              |h >= 0 = somaNeg t

tresUlt :: [a] -> [a]
tresUlt [] = []
tresUlt (x:y:w:z:t) = tresUlt (y:w:z:t)
tresUlt l = l

segundos :: [(a,b)] -> [b]
segundos [] = []
segundos (h:t) = snd h:segundos t

nosPrimeiros :: (Eq a) => a -> [(a,b)] -> Bool
nosPrimeiros x [] = False
nosPrimeiros x ((h1,h2):t) |x == h1 = True
                     |otherwise nosPrimeiros x t