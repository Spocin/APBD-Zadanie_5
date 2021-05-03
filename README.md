# Zadanie_5

This REST API was created as a part of a subject: **Aplikacje Baz Danych**

Purpous of this exercise was to present two different aproaches working with relational database.

## Database Structure

![](https://i.imgur.com/ku1OJHH.png)

## Description

Inside this API you can find two Controllers. 

### WarehouseController
This controller uses `DbService.cs` methods to create specific querries sent to database.
This approach keeps most of the SQL code inside the API. Proccesing of the querry results takes place inside API.

- This approach lets us more easily version our SQL querries but increases traffic beetwen API and database.
- API request performance may be hurt as we dont use optimalization tools that database offer

### WarehouseController2
This controller uses `Stored Procedure` to delegate work to the database. All of the SQL code is kept in database. API displays just the procedure return code.

- This approach drasticly decreases amount of code on the API side.
- We are more dependent on database performance.
- It is significantly harder to version code inside database.
