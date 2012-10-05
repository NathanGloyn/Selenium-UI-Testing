USE Northwind_Selenium
GO

DELETE	Orders
WHERE	OrderId > 11077
GO

DBCC CHECKIDENT('Orders', RESEED, 11077)
GO