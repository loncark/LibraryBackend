-- Drop tables if they exist
IF OBJECT_ID('Books', 'U') IS NOT NULL
    DROP TABLE Books;

IF OBJECT_ID('Authors', 'U') IS NOT NULL
    DROP TABLE Authors;

-- Create Authors table
CREATE TABLE Authors (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Surname VARCHAR(255) NOT NULL
);

-- Create Books table
CREATE TABLE Books (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AuthorId INT,
    Name VARCHAR(255) NOT NULL,
    FOREIGN KEY (AuthorId) REFERENCES Authors(Id)
);

-- Insert data into Authors table
INSERT INTO Authors (Name, Surname) VALUES
('William', 'Shakespeare'),
('Leo', 'Tolstoy'),
('Ernest', 'Hemingway');

-- Insert data into Books table
INSERT INTO Books (AuthorId, Name) VALUES
-- William Shakespeare's works
(1, 'Hamlet'),
(1, 'Romeo and Juliet'),
(1, 'Macbeth'),
(1, 'Othello'),
(1, 'King Lear'),

-- Leo Tolstoy's works
(2, 'War and Peace'),
(2, 'Anna Karenina'),
(2, 'The Death of Ivan Ilyich'),
(2, 'Resurrection'),
(2, 'Hadji Murat'),

-- Ernest Hemingway's works
(3, 'The Old Man and the Sea'),
(3, 'A Farewell to Arms'),
(3, 'For Whom the Bell Tolls'),
(3, 'The Sun Also Rises'),
(3, 'To Have and Have Not');
