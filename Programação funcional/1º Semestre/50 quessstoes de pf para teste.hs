enumFromTo :: Int -> Int -> [Int]
enumFromTo m n = if m<n then (m:enumFromTo (m+1) (n-1):n) 