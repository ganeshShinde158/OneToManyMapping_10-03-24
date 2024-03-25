use ApiTestOm


-- Create Stored Procedure for inserting a student with qualifications
CREATE PROCEDURE sp_InsertStudentWithQualifications
    @name VARCHAR(100),
    @email VARCHAR(100),
    @mobile VARCHAR(20),
    @city VARCHAR(40),
    @qualification VARCHAR(50),
    @university VARCHAR(100),
    @passing_year INT,
    @percentage FLOAT,
    @ResponseMsg NVARCHAR(255) OUTPUT,
    @OperationSuccess BIT OUTPUT,
    @InsertedStudentID INT OUTPUT
AS
BEGIN
    -- Declare variables
    DECLARE @studentId INT;
    DECLARE @errorOccurred BIT = 0;

    BEGIN TRY
        -- Start a transaction
        BEGIN TRANSACTION;

        -- Insert into students table
        INSERT INTO students (name, email, mobile, city)
        VALUES (@name, @email, @mobile, @city);

        -- Get the generated student ID
        SET @studentId = SCOPE_IDENTITY();

        -- Insert into student_qualifications table
        INSERT INTO student_qualifications (student_id, qualification, university, passing_year, percentage)
        VALUES (@studentId, @qualification, @university, @passing_year, @percentage);

        -- Commit the transaction
        COMMIT;

        -- Success
        SET @ResponseMsg = 'Student and qualification details inserted successfully.';
        SET @OperationSuccess = 1;
        SET @InsertedStudentID = @studentId;
    END TRY
    BEGIN CATCH
        -- An error occurred, set the flag
        SET @errorOccurred = 1;

        -- Rollback the transaction
        ROLLBACK;

        -- Set error message
        SET @ResponseMsg = ERROR_MESSAGE();
        
        -- Failure
        SET @OperationSuccess = 0;
        SET @InsertedStudentID = 0; -- Set to 0 as the insertion failed
    END CATCH;
END;


/*
DECLARE @ResponseMsg NVARCHAR(255);
DECLARE @OperationSuccess BIT;
DECLARE @InsertedStudentID INT;

EXEC sp_InsertStudentWithQualifications
    @name = 'John Doe',
    @email = 'john@example.com',
    @mobile = '1234567890',
    @city = 'CityName',
    @qualification = 'Bachelor''s Degree',
    @university = 'XYZ University',
    @passing_year = 2022,
    @percentage = 85.5,
    @ResponseMsg = @ResponseMsg OUTPUT,
    @OperationSuccess = @OperationSuccess OUTPUT,
    @InsertedStudentID = @InsertedStudentID OUTPUT;

SELECT @ResponseMsg AS ResponseMsg, @OperationSuccess AS OperationSuccess, @InsertedStudentID AS InsertedStudentID;


*/

