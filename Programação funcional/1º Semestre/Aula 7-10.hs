tirar :: Int -> [a] -> [a]
tirar 0 l = []
tirar n [] = []
tirar n (x:xs)
      | n > 0 = x : tirar (n-1) xs
      | n < 0 = []

selPos :: Int -> [a] -> a
selPos n (x:xs)
       | n == 1 = x 
       | n > 0 = selPos (n-1) xs

--(!!) :: [a] -> Int -> a
--(!!) n (x:xs)
--      |n == 0 = x
--      |n > 0 = (!!) xs (n-1)

larga :: Int -> [a] -> [a]
larga 0 l = l
larga n [] = []
larga n (x:xs)
      |n > 0 = larga (n-1) xs
      |n < 0 = x:xs

separarAt :: Int -> [a] -> ([a],[a])
separarAt n [] = ([],[])
separarAt n l | n <= 0 = ([],l)
separarAt n (x:xs) = (x:fst(separarAt (n-1) xs),snd(separarAt (n-1) xs))
--separarAt n (x:xs) = (x:l1, l2)
--         where (l1,l2) = separarAt (n-1) xs
--separarAt n (x:xs) let (a,b) = split (n-1) xs
                   in (x:a,b)