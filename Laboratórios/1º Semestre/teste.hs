data Ponto = Cartesiano Double Double | Polar {distPonto :: Double, anguloPonto :: Double} deriving (Show,Eq)

posx :: Ponto -> Double
posx ponto = case ponto of Cartesiano x _ -> x
                           Polar d a -> if a == pi/2 then 0 else d * cos a

posy :: Ponto -> Double
posy ponto = case ponto of Cartesiano _ y -> y
                           Polar d a -> if a == pi then 0 else d * sin a

raio :: Ponto -> Double
raio ponto = case ponto of Cartesiano x y -> sqrt $ x^2 + y^2
                           Polar d _ -> d

angulo :: Ponto -> Double
angulo ponto = case ponto of Cartesiano x y -> if x < 0 && y == 0 then pi else
                                               if x < 0 then pi + atan (y/x) else
                                               atan (y/x)
                             Polar _ a -> a

dist :: Ponto -> Ponto -> Double
dist ponto1 ponto2 = sqrt (((posx ponto1 - posx ponto2) ^ 2) + (posy ponto1 - posy ponto2) ^ 2)
