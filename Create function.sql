USE [AdventureWorks2012]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID (N'ufn_PersonSalesTotal', N'IF') IS NOT NULL
    DROP FUNCTION ufn_PersonSalesTotal;
GO

CREATE FUNCTION ufn_PersonSalesTotal (@FirstName [dbo].[Name], @LastName [dbo].[Name],
										@EmailAddress [nvarchar](50), @PhoneNumber [dbo].[Phone])
RETURNS TABLE
AS
RETURN
(
	select FirstName as 'First Name', LastName as 'Last Name', EmailAddress as 'Email', PhoneNumber as 'Phone', OrdersCount as 'Orders count'
			, LinesCount as 'Total lines', ItemsQty 'Items quantity', TotalPrice as 'Total price'
	 from [Person].[Person] p
	 inner join [Person].[PersonPhone] phone on phone.BusinessEntityID = p.BusinessEntityID
	 inner join [Person].[EmailAddress] mail on mail.BusinessEntityID = p.BusinessEntityID
	 inner join [Sales].[Customer] cust on cust.PersonID = p.BusinessEntityID
	 inner join (
					select soh.CustomerID, count(sod2.SalesOrderID) as OrdersCount, sum(LinesCount) as LinesCount, sum(ItemsQty) as ItemsQty, sum(TotalPrice) as TotalPrice
					 from [Sales].[SalesOrderHeader] soh
					 inner join [Sales].[SalesOrderDetail] sod2 on sod2.SalesOrderID = soh.SalesOrderID
					 inner join (
									select SalesOrderID, count(OrderQty) as LinesCount, sum(OrderQty) as ItemsQty , sum(UnitPrice) as TotalPrice--, sum(LineTotal) as FullCost
									from [Sales].[SalesOrderDetail]
									group by SalesOrderID
								) OrdersTotal on OrdersTotal.SalesOrderID = sod2.SalesOrderID 
					 group by soh.CustomerID
					) Totals on Totals.CustomerID = cust.CustomerID
	 where (@FirstName = '' or FirstName = @FirstName) and (@LastName = '' or LastName = @LastName)
			and (@EmailAddress = '' or EmailAddress = @EmailAddress) and (@PhoneNumber = '' or PhoneNumber = @PhoneNumber)
);
GO