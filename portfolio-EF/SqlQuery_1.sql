USE [portfolio-EF-test0]
GO

SELECT [Transaction_TransactionId]
      ,[Coin_CoinId]
  FROM [dbo].[TransactionCoins]

GO


select * from [dbo].[TransactionCoins]
select * from [dbo].[Transactions]
select * from [dbo].Coins

select * 
from Coins 
join TransactionCoins
on Coins.CoinId = TransactionCoins.Coin_CoinId
join Transactions 
on Transactions.TransactionId = TransactionCoins.Transaction_TransactionId
where CoinId = 3

