-- ============================================================
-- HR Management Project - Oracle SQL
-- Final reviewed version based on the instructor requirements
-- ============================================================

-- Optional cleanup if objects already exist should be done manually before rerun.

-- =========================
-- 1) CREATE TABLES
-- =========================
CREATE TABLE departments (
    DepartmentID INT PRIMARY KEY,
    DepartmentName VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE employees (
    EmployeeID   INT PRIMARY KEY,
    FirstName    VARCHAR(20) NOT NULL,
    LastName     VARCHAR(30) NOT NULL,
    DepartmentID INT,
    ManagerID    INT,
    Salary       DECIMAL(10,2),
    CONSTRAINT employees_Salary_CK CHECK (Salary > 0),
    CONSTRAINT employees_DepartmentID_FK
        FOREIGN KEY (DepartmentID) REFERENCES departments(DepartmentID),
    CONSTRAINT employees_ManagerID_FK
        FOREIGN KEY (ManagerID) REFERENCES employees(EmployeeID)
);

CREATE TABLE LeaveRequests (
    LeaveID    INT PRIMARY KEY,
    EmployeeID INT,
    StartDate  DATE NOT NULL,
    EndDate    DATE NOT NULL,
    Reason     VARCHAR(100),
    Status     VARCHAR(20),
    CONSTRAINT LeaveRequests_Status_CK
        CHECK (Status IN ('Pending','Approved','Rejected')),
    CONSTRAINT LeaveRequests_EmployeeID_FK
        FOREIGN KEY (EmployeeID) REFERENCES employees(EmployeeID),
    CONSTRAINT LeaveRequests_Date_CK
        CHECK (EndDate >= StartDate)
);

CREATE TABLE Instructors (
    InstructorID INT PRIMARY KEY,
    FirstName    VARCHAR(25) NOT NULL,
    LastName     VARCHAR(25) NOT NULL
);

CREATE TABLE TrainingCourses (
    CourseID     INT PRIMARY KEY,
    CourseName   VARCHAR(100) NOT NULL,
    StartDate    DATE NOT NULL,
    EndDate      DATE NOT NULL,
    InstructorID INT,
    CONSTRAINT CK_TrainingCourses_EndDate CHECK (EndDate > StartDate),
    CONSTRAINT TrainingCourses_InstructorID_FK
        FOREIGN KEY (InstructorID) REFERENCES Instructors(InstructorID)
);

-- =========================
-- 2) INSERT DATA
-- =========================

INSERT INTO departments VALUES (1,'HR');
INSERT INTO departments VALUES (2,'Finance');
INSERT INTO departments VALUES (3,'IT');
INSERT INTO departments VALUES (4,'Marketing');
INSERT INTO departments VALUES (5,'Operations');
INSERT INTO departments VALUES (6,'Sales');
INSERT INTO departments VALUES (7,'Customer Service');
INSERT INTO departments VALUES (8,'Research and Development');
INSERT INTO departments VALUES (9,'Legal');



-- Employees are inserted in an order that satisfies the self-referencing ManagerID FK.
INSERT INTO employees VALUES (1,'John','Doe',1,NULL,50000);
INSERT INTO employees VALUES (2,'Jane','Smith',2,1,60000);
INSERT INTO employees VALUES (3,'Ahmad','Ali',3,1,55000);
INSERT INTO employees VALUES (4,'Sara','Khan',2,3,52000);
INSERT INTO employees VALUES (5,'Mohammed','Hassan',1,1,48000);
INSERT INTO employees VALUES (6,'Hasan','Ali',1,1,54000);
INSERT INTO employees VALUES (7,'Fatima','Khaled',2,1,62000);
INSERT INTO employees VALUES (8,'Amr','Mohammed',3,1,56000);
INSERT INTO employees VALUES (9,'Sara','Ahmed',4,1,51000);
INSERT INTO employees VALUES (10,'Ahmed','Youssef',1,1,50000);


INSERT INTO Instructors VALUES (1,'Sarah','Johnson');
INSERT INTO Instructors VALUES (2,'James','Smith');
INSERT INTO Instructors VALUES (3,'Mary','Davis');
INSERT INTO Instructors VALUES (4,'Ahmad','Hassan');
INSERT INTO Instructors VALUES (5,'Fatima','Ali');
INSERT INTO Instructors VALUES (6,'Yasmine','Ali');
INSERT INTO Instructors VALUES (7,'Mohammed','Hassen');
INSERT INTO Instructors VALUES (8,'Ahmad','Youssef');
INSERT INTO Instructors VALUES (9,'Amr','Mohammed');
INSERT INTO Instructors VALUES (10,'Sara','Ahmad');


INSERT INTO TrainingCourses VALUES (201,'Leadership Skills', '2023-11-02', '2023-11-07',1);
INSERT INTO TrainingCourses VALUES (202,'Excel Advanced', '2023-11-12', '2023-11-14',2);
INSERT INTO TrainingCourses VALUES (203,'Time Management', '2023-11-20', '2023-11-21',3);
INSERT INTO TrainingCourses VALUES (204,'Communication Tips', '2023-12-02', '2023-12-04',4);
INSERT INTO TrainingCourses VALUES (205,'Conflict Resolution', '2023-12-10', '2023-12-11',5);
INSERT INTO TrainingCourses VALUES (206,'Negotiation Skills', '2023-12-20', '2023-12-22',6);
INSERT INTO TrainingCourses VALUES (207,'Financial Planning', '2024-01-05', '2024-01-07',7);
INSERT INTO TrainingCourses VALUES (208,'Leadership Workshop', '2024-01-10', '2024-01-12',8);
INSERT INTO TrainingCourses VALUES (209,'Effective Communication', '2024-01-15', '2024-01-17',9);
INSERT INTO TrainingCourses VALUES (210,'Time Management', '2024-01-20', '2024-01-22',10);


INSERT INTO LeaveRequests VALUES (106,6, '2023-12-10', '2023-12-24','Annual Leave','Approved');
INSERT INTO LeaveRequests VALUES (102,3, '2023-11-05', '2023-11-07','Conference','Pending');
INSERT INTO LeaveRequests VALUES (103,2, '2023-10-20', '2023-10-21','Sick Leave','Approved');
INSERT INTO LeaveRequests VALUES (104,4, '2023-12-01', '2023-12-03','Family Emergency','Pending');
INSERT INTO LeaveRequests VALUES (105,5, '2023-10-25', '2023-10-26','Personal','Approved');



SELECT * FROM LeaveRequests;

-- =========================
-- 3) UPDATE / DELETE TASKS
-- Same task names and values from the original project file
-- =========================

-- task1: Update salary
UPDATE employees
SET Salary = 52000
WHERE EmployeeID = 5;

-- task2: Rename department
UPDATE departments
SET DepartmentName = 'Human Resources'
WHERE DepartmentName = 'HR';

-- task3: Update ManagerID
UPDATE employees
SET ManagerID = 10
WHERE ManagerID = 3;

-- task4: Update course start date
UPDATE TrainingCourses
SET StartDate =  '2023-11-13'
WHERE CourseID = 202;

-- task5: Delete employee
DELETE FROM employees
WHERE EmployeeID = 8;

-- task6: Delete department
DELETE FROM departments
WHERE DepartmentID = 9;

-- task7: Delete training course
DELETE FROM TrainingCourses
WHERE CourseID = 205;


-- ============================================================
-- 4) SIMPLE QUERIES
-- Instructor requirements covered completely.
-- ============================================================

-- Q1: Employee names and salaries
SELECT FirstName + ' ' + LastName AS "Employee Name",
       Salary
FROM employees;

-- Q2: Average salary
SELECT ROUND(AVG(Salary), 2) AS "Average Salary"
FROM employees;

-- Q3: Department names
SELECT DepartmentName
FROM departments;

-- Q4: Training course names and start dates
SELECT CourseName,
       StartDate
FROM TrainingCourses;

-- ============================================================
-- 5) COMPLEX QUERIES - 15 TASKS
-- ============================================================

-- Task 1: Number of employees in each department with average salary
SELECT d.DepartmentName,
       COUNT(e.EmployeeID) AS "Total Employees",
       ROUND(AVG(e.Salary), 2) AS "Average Salary"
FROM departments d
LEFT JOIN employees e
    ON e.DepartmentID = d.DepartmentID
GROUP BY d.DepartmentID, d.DepartmentName
ORDER BY d.DepartmentID;

-- Task 2: Instructor names and the courses they teach
SELECT i.FirstName + ' ' + i.LastName AS "Instructor Name",
       t.CourseName
FROM Instructors i
JOIN TrainingCourses t
    ON i.InstructorID = t.InstructorID
ORDER BY i.InstructorID, t.CourseID;

-- Task 3: Employee names, departments, and the average salary in each department
SELECT e.FirstName + ' ' + e.LastName AS "Employee Name",
       d.DepartmentName,
       ROUND((
           SELECT AVG(e2.Salary)
           FROM employees e2
           WHERE e2.DepartmentID = e.DepartmentID
       ), 2) AS "Department Average Salary"
FROM employees e
JOIN departments d
    ON e.DepartmentID = d.DepartmentID
ORDER BY d.DepartmentID, e.EmployeeID;

-- Task 4: Number of approved and rejected leave requests for each employee
SELECT e.EmployeeID,
       e.FirstName + ' ' + e.LastName AS "Employee Name",
       SUM(CASE WHEN l.Status = 'Approved' THEN 1 ELSE 0 END) AS "Approved Leaves",
       SUM(CASE WHEN l.Status = 'Rejected' THEN 1 ELSE 0 END) AS "Rejected Leaves"
FROM employees e
LEFT JOIN LeaveRequests l
    ON e.EmployeeID = l.EmployeeID
GROUP BY e.EmployeeID, e.FirstName, e.LastName
ORDER BY e.EmployeeID;

-- Task 5: Number of employees in each department
SELECT d.DepartmentID,
       d.DepartmentName,
       COUNT(e.EmployeeID) AS "Total Employees"
FROM departments d
LEFT JOIN employees e
    ON e.DepartmentID = d.DepartmentID
GROUP BY d.DepartmentID, d.DepartmentName
ORDER BY d.DepartmentID;

-- Task 6: Instructor name and number of courses they teach
SELECT i.InstructorID,
       i.FirstName + ' ' + i.LastName AS "Instructor Name",
       COUNT(t.CourseID) AS "Total Courses"
FROM Instructors i
LEFT JOIN TrainingCourses t
    ON i.InstructorID = t.InstructorID
GROUP BY i.InstructorID, i.FirstName, i.LastName
ORDER BY i.InstructorID;

-- Task 7: Employee names and the department names they work in
SELECT e.FirstName + ' ' + e.LastName AS "Employee Name",
       d.DepartmentName
FROM employees e
JOIN departments d
    ON e.DepartmentID = d.DepartmentID
ORDER BY d.DepartmentID, e.EmployeeID;

-- Task 8: Employees who work in departments that have more than 3 employees
SELECT e.FirstName + ' ' + e.LastName AS "Employee Name",
       d.DepartmentName
FROM employees e
JOIN departments d
    ON e.DepartmentID = d.DepartmentID
WHERE e.DepartmentID IN (
    SELECT DepartmentID
    FROM employees
    GROUP BY DepartmentID
    HAVING COUNT(EmployeeID) > 3
)
ORDER BY d.DepartmentID, e.EmployeeID;

-- Task 9: Employees who share the same department with other employees
SELECT e.FirstName + ' ' + e.LastName AS "Employee Name",
       d.DepartmentName
FROM employees e
JOIN departments d
    ON e.DepartmentID = d.DepartmentID
WHERE e.DepartmentID IN (
    SELECT DepartmentID
    FROM employees
    GROUP BY DepartmentID
    HAVING COUNT(EmployeeID) > 1
)
ORDER BY d.DepartmentID, e.EmployeeID;

-- Task 10: Training courses taught by instructor Sarah Johnson
SELECT t.CourseID,
       t.CourseName,
       t.StartDate,
       t.EndDate
FROM TrainingCourses t
JOIN Instructors i
    ON t.InstructorID = i.InstructorID
WHERE i.FirstName = 'Sarah'
  AND i.LastName = 'Johnson'
ORDER BY t.CourseID;

-- Task 11: Employees who have not been assigned a manager yet
SELECT EmployeeID,
       FirstName + ' ' + LastName AS "Employee Name",
       DepartmentID,
       Salary
FROM employees
WHERE ManagerID IS NULL
ORDER BY EmployeeID;

-- Task 12: Department names and total employee salaries in each department
SELECT d.DepartmentName,
       ISNULL(SUM(e.Salary), 0) AS "Total Salary"
FROM departments d
LEFT JOIN employees e
    ON d.DepartmentID = e.DepartmentID
GROUP BY d.DepartmentID, d.DepartmentName
ORDER BY d.DepartmentID;

-- Task 13: Department and employee receiving the highest salary in each department
SELECT d.DepartmentName,
       e.FirstName + ' ' + e.LastName AS "Employee Name",
       e.Salary
FROM departments d
JOIN employees e
    ON d.DepartmentID = e.DepartmentID
WHERE e.Salary = (
    SELECT MAX(e2.Salary)
    FROM employees e2
    WHERE e2.DepartmentID = e.DepartmentID
)
ORDER BY d.DepartmentID, e.EmployeeID;

-- Task 14: Number of training courses that start after 2023-12-01
SELECT COUNT(*) AS "Total Courses After 2023-12-01"
FROM TrainingCourses
WHERE StartDate >  '2023-12-01';

-- Task 15: Employees whose salary is greater than the average salary in their department
SELECT e.FirstName + ' ' + e.LastName AS "Employee Name",
       e.Salary,
       d.DepartmentName
FROM employees e
JOIN departments d
    ON d.DepartmentID = e.DepartmentID
WHERE e.Salary > (
    SELECT AVG(e2.Salary)
    FROM employees e2
    WHERE e2.DepartmentID = e.DepartmentID
)
ORDER BY d.DepartmentID, e.Salary DESC;

-- ============================================================
-- END OF PROJECT
-- ============================================================
