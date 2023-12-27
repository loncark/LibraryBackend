-- Drop tables if they exist
IF OBJECT_ID('Books', 'U') IS NOT NULL
    DROP TABLE Books;

IF OBJECT_ID('Authors', 'U') IS NOT NULL
    DROP TABLE Authors;

IF OBJECT_ID('Users', 'U') IS NOT NULL
    DROP TABLE Users;

CREATE TABLE Authors (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Surname VARCHAR(255) NOT NULL
);

CREATE TABLE Books (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AuthorId INT,
    Name VARCHAR(255) NOT NULL,
    FOREIGN KEY (AuthorId) REFERENCES Authors(Id)
);

CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(255) NOT NULL,
    PasswordHash VARBINARY(MAX) NOT NULL,
    PasswordSalt VARBINARY(MAX) NOT NULL
);

INSERT INTO Authors (Name, Surname) VALUES
('William', 'Shakespeare'),
('Leo', 'Tolstoy'),
('Ernest', 'Hemingway');

INSERT INTO Books (AuthorId, Name) VALUES
(1, 'Hamlet'),
(1, 'Romeo and Juliet'),
(1, 'Macbeth'),
(1, 'Othello'),
(1, 'King Lear'),

(2, 'War and Peace'),
(2, 'Anna Karenina'),
(2, 'The Death of Ivan Ilyich'),
(2, 'Resurrection'),
(2, 'Hadji Murat'),

(3, 'The Old Man and the Sea'),
(3, 'A Farewell to Arms'),
(3, 'For Whom the Bell Tolls'),
(3, 'The Sun Also Rises'),
(3, 'To Have and Have Not');
