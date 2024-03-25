Create Database ApiTestOm
use ApiTestOm

-- Create Student table
CREATE TABLE students (
    rno INT IDENTITY PRIMARY KEY,
    name VARCHAR(100),
    email VARCHAR(100),
    mobile VARCHAR(20),
    city VARCHAR(40)
);

-- Create Student Qualifications table
CREATE TABLE student_qualifications (
    qualification_id INT IDENTITY PRIMARY KEY,
    student_id INT CONSTRAINT fkssditid REFERENCES students(rno),
    qualification VARCHAR(50),
    university VARCHAR(100),
    passing_year INT,
    percentage FLOAT
);
select * from students
select * from student_qualifications

delete students