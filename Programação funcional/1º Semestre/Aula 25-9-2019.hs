second :: [a] -> a
second l = head (tail l)

snap :: (a,b) -> (b,a)
snap (x,y) = (y,x)

pair :: a -> b -> (a,b)
pair x y = (x,y)

double :: (Num a) => a ->a
double x = x * 2


f x = x + 3
twice :: (a -> a) -> a -> a
twice f x = f (f x)

type String = [Char]

type Coordenada = (Float, Float)
distancia :: Coordenada -> Coordenada -> Float
distancia (x1, y1) (x2, y2) = sqrt ((x2 - x1)^2 + (y2 - y1)^2)

type Triplos a = (a, a, a)
primeiro :: Triplos Int -> Int
primeiro (x, y, z) = x

data Bool = False | True
data Cor = Azul | Amarelo | Verde | Vermelho
fria :: Cor -> Bool
