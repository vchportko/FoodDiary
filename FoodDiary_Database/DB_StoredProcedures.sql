USE [FoodDiary]
GO

--------------------SELECTS--------------------
--checking if procedure 'loginUser' exsists
IF OBJECT_ID('loginUser') IS NOT NULL
	DROP PROCEDURE loginUser
GO

CREATE PROCEDURE loginUser
	@login VARCHAR(20),
	@password VARCHAR(50)
AS
	SELECT TOP(1) us.id
	FROM Users us
	WHERE us.user_password LIKE @password AND us.user_login LIKE @login
GO

--checking if procedure 'selectCategories' exsists
IF OBJECT_ID('selectCategories') IS NOT NULL
	DROP PROCEDURE selectCategories
GO

CREATE PROCEDURE selectCategories
	@category VARCHAR(20)
AS
	DECLARE @contain VARCHAR(20) = '%' + @category + '%'
	SELECT prc.name
	FROM ProductCategories prc
	WHERE prc.name LIKE @contain
GO

--checking if procedure 'selectSubcategoriesByCategory' exsists
IF OBJECT_ID('selectSubcategoriesByCategory') IS NOT NULL
	DROP PROCEDURE selectSubcategoriesByCategory
GO

CREATE PROCEDURE selectSubcategoriesByCategory
	@category VARCHAR(20),
	@subcategory VARCHAR(20)
AS
	DECLARE @contain VARCHAR(20) = '%' + @subcategory + '%'
	SELECT prsc.name
	FROM ProductSubcategories prsc
	INNER JOIN ProductCategories prc
	ON prc.id = prsc.productCategoty_id
	WHERE prc.name = @category	AND prsc.name LIKE @contain
GO

--checking if procedure 'selectProductsBySubcategory' exsists
IF OBJECT_ID('selectProductsBySubcategory') IS NOT NULL
	DROP PROCEDURE selectProductsBySubcategory
GO

CREATE PROCEDURE selectProductsBySubcategory
	@subcategory VARCHAR(20),
	@product VARCHAR(50)
AS
	DECLARE @contain VARCHAR(20) = '%' + @product + '%'
	SELECT pr.name, prsc.name, pr.calories_in100g, pr.fat_in100g, pr.protein_in100g, pr.arbohydrates_in100g
	FROM Products pr
	INNER JOIN ProductSubcategories prsc
	ON prsc.id = pr.subcategory_id
	WHERE prsc.name LIKE @subcategory AND pr.name LIKE @contain
GO

--checking if procedure 'selectSumsOfCaloriesForLastWeek' exsists
IF OBJECT_ID('selectSumsOfCalories') IS NOT NULL
	DROP PROCEDURE selectSumsOfCalories
GO

CREATE PROCEDURE selectSumsOfCalories
	@user_id INT,
	@numOfDays INT
AS
	IF @numOfDays != 0
		BEGIN
			SELECT Dat, sum(Calories) AS SumCalories
			FROM SumsInProducts
			WHERE UserId = @user_id AND DAT>=DATEADD(DAY,-7,GETDATE())	
			GROUP BY Dat
		END
	ELSE
		BEGIN
			SELECT Dat, sum(Calories) AS SumCalories
			FROM SumsInProducts
			WHERE UserId = @user_id	
			GROUP BY Dat
		END
GO

--checking if procedure 'selectSumsOfFat' exsists
IF OBJECT_ID('selectSumsOfFat') IS NOT NULL
	DROP PROCEDURE selectSumsOfFat
GO

CREATE PROCEDURE selectSumsOfFat
	@user_id INT,
	@numOfDays INT
AS
	IF @numOfDays != 0
		BEGIN
			SELECT Dat, sum(Fat) AS SumFat
			FROM SumsInProducts
			WHERE UserId = @user_id AND DAT>=DATEADD(DAY,-7,GETDATE())	
			GROUP BY Dat
		END
	ELSE
		BEGIN
			SELECT Dat, sum(Fat) AS SumFat
			FROM SumsInProducts
			WHERE UserId = @user_id	
			GROUP BY Dat
		END
GO

--checking if procedure 'selectSumsOfProtein' exsists
IF OBJECT_ID('selectSumsOfProtein') IS NOT NULL
	DROP PROCEDURE selectSumsOfProtein
GO

CREATE PROCEDURE selectSumsOfProtein
	@user_id INT,
	@numOfDays INT
AS
	IF @numOfDays != 0
		BEGIN
			SELECT Dat, sum(Protein) AS SumProtein
			FROM SumsInProducts
			WHERE UserId = @user_id AND DAT>=DATEADD(DAY,-7,GETDATE())	
			GROUP BY Dat
		END
	ELSE
		BEGIN
			SELECT Dat, sum(Protein) AS SumProtein
			FROM SumsInProducts
			WHERE UserId = @user_id	
			GROUP BY Dat
		END
GO

--checking if procedure 'selectSumsOfCarbo' exsists
IF OBJECT_ID('selectSumsOfCarbo') IS NOT NULL
	DROP PROCEDURE selectSumsOfCarbo
GO

CREATE PROCEDURE selectSumsOfCarbo
	@user_id INT,
	@numOfDays INT
AS
	IF @numOfDays != 0
		BEGIN
			SELECT Dat, sum(Carbo) AS SumCarbo
			FROM SumsInProducts
			WHERE UserId = @user_id AND DAT>=DATEADD(DAY,-7,GETDATE())	
			GROUP BY Dat
		END
	ELSE
		BEGIN
			SELECT Dat, sum(Carbo) AS SumCarbo
			FROM SumsInProducts
			WHERE UserId = @user_id	
			GROUP BY Dat
		END
GO

--checking if procedure 'selectSumsOfCalories' exsists
IF OBJECT_ID('selectSumsOfCalories') IS NOT NULL
	DROP PROCEDURE selectSumsOfCalories
GO

CREATE PROCEDURE selectSumsOfCalories
	@user_id INT,
	@numOfDays INT
AS
	IF @numOfDays != 0
		BEGIN
			SELECT Dat, sum(Calories) AS SumCalories
			FROM SumsInProducts
			WHERE UserId = @user_id AND DAT>=DATEADD(DAY,-7,GETDATE())	
			GROUP BY Dat
		END
	ELSE
		BEGIN
			SELECT Dat, sum(Calories) AS SumCalories
			FROM SumsInProducts
			WHERE UserId = @user_id	
			GROUP BY Dat
		END
GO



--checking if procedure 'selectProdOfUserByDay' exsists
IF OBJECT_ID('selectProdOfUserByDay') IS NOT NULL
	DROP PROCEDURE selectProdOfUserByDay
GO

CREATE PROCEDURE selectProdOfUserByDay
	@user_id INT,
	@date DATE
AS
	SELECT prc.name, prsub.name, pr.name,pr.calories_in100g,pr.fat_in100g, pr.protein_in100g, pr.arbohydrates_in100g, usn.number_of_grams
	FROM [dbo].[Users] us
	INNER JOIN [dbo].[UserNutritions] usn
	ON us.id = usn.users_id
	INNER JOIN [dbo].[Products] pr
	ON pr.id=usn.product_id
	INNER JOIN [dbo].[ProductSubcategories] prsub
	ON prsub.id = pr.subcategory_id
	INNER JOIN [dbo].[ProductCategories] prc
	ON prsub.productCategoty_id = prc.id
	WHERE us.id = @user_id AND usn.date_of_nutrition = @date
GO

--------------------INSERTS--------------------
--checking if procedure 'addCategory' exsists
IF OBJECT_ID('addCategory') IS NOT NULL
	DROP PROCEDURE addCategory
GO

CREATE PROCEDURE addCategory
	@catName VARCHAR(20)
AS
	DECLARE @return INT
	BEGIN TRAN
		BEGIN TRY
			INSERT INTO ProductCategories(name)
			VALUES (@catName)
			SET @return = 1
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
			SET @return = 0
		END CATCH
	RETURN @return
GO

--checking if procedure 'addSubcategory' exsists
IF OBJECT_ID('addSubcategory') IS NOT NULL
	DROP PROCEDURE addSubcategory
GO

CREATE PROCEDURE addSubcategory
	@catName VARCHAR(20),
	@subcatName VARCHAR(20)
AS
	DECLARE @catId INT
	DECLARE @return INT
	BEGIN TRAN
		BEGIN TRY
			SET @catID = 
			(
				SELECT TOP(1) id FROM ProductCategories WHERE name = @catName
			)
			INSERT INTO ProductSubcategories(name,productCategoty_id)
			VALUES (@subcatName,@catId)
			SET @return = 1
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
			SET @return = 0
		END CATCH
	RETURN @return
GO

--checking if procedure 'addProduct' exsists
IF OBJECT_ID('addProduct') IS NOT NULL
	DROP PROCEDURE addProduct
GO

CREATE PROCEDURE addProduct
	@productName VARCHAR(50),
	@subcategoryName VARCHAR(20),
	@calories_in100g FLOAT,
	@fat_in100g FLOAT,
	@protein_in100g FLOAT,
	@carbo_in100g FLOAT
AS
	DECLARE @subcatId INT
	DECLARE @return INT = 0
	BEGIN TRAN
		BEGIN TRY
			SET @subcatID = 
			(
				SELECT TOP(1) id FROM ProductSubcategories WHERE name = @subcategoryName
			)
			INSERT INTO Products(name,subcategory_id,calories_in100g,fat_in100g,arbohydrates_in100g,protein_in100g)
			VALUES (@productName,@subcatId,@calories_in100g,@fat_in100g,@carbo_in100g,@protein_in100g)
			SET @return = 1
			print 1
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
			SET @return =  0
		END CATCH
	RETURN @return			
GO

--checking if procedure 'addProductToNutrition' exsists
IF OBJECT_ID('addProductToNutrition') IS NOT NULL
	DROP PROCEDURE addProductToNutrition
GO

CREATE PROCEDURE addProductToNutrition
	@userId INT,
	@dat DATE,
	@prodName VARCHAR(50),
	@number FLOAT
AS
	DECLARE @prodId INT
	DECLARE @return INT
	BEGIN TRAN
		BEGIN TRY
			SET @prodId = 
			(
				SELECT TOP(1) id FROM Products WHERE name = @prodName
			)
			IF NOT EXISTS (SELECT users_id FROM UserNutritions WHERE users_id = @userId AND product_id = @prodId  AND date_of_nutrition = @dat)
			BEGIN	
				INSERT INTO UserNutritions(users_id,product_id,date_of_nutrition,number_of_grams)
				VALUES (@userId,@prodId,@dat,@number)
				SET @return = 1
			END
			ELSE
			BEGIN
				UPDATE UserNutritions
				SET number_of_grams += @number
				WHERE users_id = @userId AND date_of_nutrition = @dat AND product_id = @prodId
				SET @return = 2
			END
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
			SET @return = 0
		END CATCH
	print @return
	RETURN @return
GO

--------------------UPDATES--------------------
--checking if procedure 'updateProductInNutrition' exsists
IF OBJECT_ID('updateProductInNutrition') IS NOT NULL
	DROP PROCEDURE updateProductInNutrition
GO

CREATE PROCEDURE updateProductInNutrition
	@userId INT,
	@dat DATE,
	@productSubcat VARCHAR(20),
	@prodName VARCHAR(50),
	@number FLOAT
AS
	DECLARE @prodId INT
	DECLARE @subcatId INT
	DECLARE @return INT
	SET NOCOUNT ON
	BEGIN TRAN
		BEGIN TRY
			SET @subcatId=
			(
				SELECT TOP(1) id FROM ProductSubcategories WHERE name = @productSubcat
			)
			SET @prodId = 
			(
				SELECT TOP(1) id FROM Products WHERE name = @prodName AND subcategory_id = @subcatId
			)
			UPDATE UserNutritions
			SET product_id = @prodId, number_of_grams = @number
			WHERE users_id = @userId AND date_of_nutrition = @dat
			SET @return = 1
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
			SET @return = 0
		END CATCH
	SET NOCOUNT OFF
	RETURN @return
GO

--------------------DROPS--------------------
--checking if procedure 'dropProductInNutrition' exsists
IF OBJECT_ID('dropProductInNutrition') IS NOT NULL
	DROP PROCEDURE dropProductInNutrition
GO

CREATE PROCEDURE dropProductInNutrition
	@userId INT,
	@dat DATE,
	@prodName VARCHAR(50)
AS
	DECLARE @prodId INT
	DECLARE @return INT
	SET NOCOUNT ON
	BEGIN TRAN
		BEGIN TRY
			SET @prodId = 
			(
				SELECT TOP(1) id FROM Products WHERE name = @prodName
			)
			DELETE FROM UserNutritions
			WHERE users_id = @userId AND product_id = @prodId AND date_of_nutrition = @dat
			SET @return = 1
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
			SET @return = 0
		END CATCH
	SET NOCOUNT OFF
	RETURN @return
GO

