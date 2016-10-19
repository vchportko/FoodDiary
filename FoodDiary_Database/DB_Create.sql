--checking if database exists
IF DB_ID('FoodDiary') IS NULL
	CREATE DATABASE FoodDiary
GO

USE [FoodDiary]

--checking if table 'Users' exists
IF OBJECT_ID('Users') IS NOT NULL
	DROP TABLE Users

CREATE TABLE Users
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	user_login VARCHAR(50) NOT NULL UNIQUE,
	user_password VARCHAR(50) NOT NULL,
)
GO

--checking if table 'ProductCategories' exists
IF OBJECT_ID('ProductCategories') IS NOT NULL
	DROP TABLE ProductCategories

CREATE TABLE ProductCategories
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	name VARCHAR(20) NOT NULL UNIQUE,
)
GO

--checking if table 'ProductCategories' exists
IF OBJECT_ID('ProductSubcategories') IS NOT NULL
	DROP TABLE ProductSubcategories

CREATE TABLE ProductSubcategories
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	productCategoty_id INT NOT NULL FOREIGN KEY REFERENCES ProductCategories(id),
	name VARCHAR(20) NOT NULL
)

GO

--checking if table 'Products' exists
IF OBJECT_ID('Products') IS NOT NULL
	DROP TABLE Products

CREATE TABLE Products
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	name VARCHAR(50) NOT NULL,
	calories_in100g FLOAT NOT NULL CHECK (calories_in100g >= 0),
	subcategory_id INT NOT NULL REFERENCES ProductSubcategories(id),	
	fat_in100g FLOAT CHECK (fat_in100g >= 0),
	protein_in100g FLOAT CHECK (protein_in100g >= 0),
	arbohydrates_in100g FLOAT CHECK (arbohydrates_in100g >= 0)	
)
GO

--checking if table 'UserNutritions' exists
IF OBJECT_ID('UserNutritions') IS NOT NULL
	DROP TABLE UserNutritions

CREATE TABLE UserNutritions
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	users_id INT NOT NULL REFERENCES Users(id),
	product_id INT NOT NULL REFERENCES Products(id),
	date_of_nutrition DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	number_of_grams INT NOT NULL
)
GO

--checking if view 'SumsInProducts' exists
IF OBJECT_ID('SumsInProducts') IS NOT NULL
	DROP VIEW SumsInProducts
GO
CREATE VIEW SumsInProducts
AS
SELECT us.id AS UserId, usn.date_of_nutrition AS Dat,prc.name AS Category, prsub.name AS SubCategory, pr.name AS Product, usn.number_of_grams AS Number,
		usn.number_of_grams*pr.calories_in100g/100 AS Calories, usn.number_of_grams*pr.fat_in100g/100 AS Fat,
		usn.number_of_grams*pr.protein_in100g/100 AS Protein, usn.number_of_grams*pr.arbohydrates_in100g/100 AS Carbo
	FROM [dbo].[Users] us
	INNER JOIN [dbo].[UserNutritions] usn
	ON us.id = usn.users_id
	INNER JOIN [dbo].[Products] pr
	ON pr.id=usn.product_id
	INNER JOIN [dbo].[ProductSubcategories] prsub
	ON prsub.id = pr.subcategory_id
	INNER JOIN [dbo].[ProductCategories] prc
	ON prsub.productCategoty_id = prc.id
GO



