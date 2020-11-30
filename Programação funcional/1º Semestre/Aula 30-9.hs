
cabeça :: [a] -> a
cabeça(x:xs) = x

cauda :: [a] -> [a]
cauda(x:xs) = xs

segundo :: [a] -> a
segundo(x:y:z) = y

comprimento :: [a] -> Int
comprimento [] = 0
comprimento(x:xs) = 1 + comprimento xs

fact 0 = 1
fact n |n > 0 = n * fact(n-1)

sum :: (Num a) => [a] -> a
sum [] = 0
sum(h:t) = h + sum t

--elem :: (Eq a) => a -> [a]
--elem x [] = False
--elem x (h:t) = if X == h then True
--	                     else (elem x t)

--elem x [] = False
--elem x (h:t)
--		|x == h = True
--		|otherwise = elem x t

elem x [] = False
elem x (h:t) = (x == h)||(elem x t)