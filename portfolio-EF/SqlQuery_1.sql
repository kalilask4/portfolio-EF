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



select * from Transactions
where
transactionId=1

select c.CoinId, c.Name, t.TransactionId, t.TransactionSymbol 
from Coins as c
join
(select * from TransactionCoins) as tc
on
c.CoinId = tc.Coin_CoinId
join
(select * from Transactions) as t
on
t.TransactionId = tc.Coin_CoinId
where t.TransactionId=2



select * from Coins
select * from Transactions
select * from TransactionCoins

select c.CoinId, c.Name, t.TransactionId, t.TransactionSymbol
from Coins as c
join
(select * from TransactionCoins) as tc
on
c.CoinId = tc.Coin_CoinId
join
(select * from Transactions) as t
on
t.TransactionId = tc.Coin_CoinId


insert into Coins
(Name, Symbol, Amount, CurrentPrice, ValueUSD, DateUpdate, AveragePurchasePrice) values
(N'AAA', N'A0', 100, 2, 200, GETDATE(), 100)


insert into Transactions
(Side, TransactionSymbol, Amount, Priсe, AddDate) values
(N'AAACO8', N'Buy', 1, 320, GETDATE())


insert into TransactionCoins
(Transaction_TransactionId, Coin_CoinId) values
(1,17)