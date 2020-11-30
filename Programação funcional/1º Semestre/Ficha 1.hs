

max2 :: Integer -> Integer -> Integer
max2 x y = if x > y then x else y
max3 :: Integer -> Integer -> Integer -> Integer
max3 x y z = if max2 x y > z then max2 x y else z


perimetro :: Integer -> Integer
perimetro raio = 2 * 3.14 * raio



nRaizes :: Integer -> Integer -> Integer -> t
nRaizes a b c
	|delta > 0 = 2
	|delta == 0 = 1
	|delta < 0 = 0
	where delta = b^2 - 4*a*c 




raizes a b c
	| n == 2 = [x1, x2]
	| n == 1 = [x1]
	| n == 0 = []
	where n = nRaizes a b c
	      delta = b^2 - 4*a*c
	      (x1,x2) = (((-b) + sqrt (delta))/ (2*a), ((-b) - sqrt (delta))/(2*a))
