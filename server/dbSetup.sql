-- Active: 1760459935767@@mysql.codeworksacademy.com@3306@intuitive_elephant_ef9f_db
CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name VARCHAR(255) COMMENT 'User Name',
    email VARCHAR(255) UNIQUE COMMENT 'User Email',
    picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

CREATE TABLE recipes (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    title VARCHAR(255) NOT NULL,
    instructions VARCHAR(5000) NOT NULL,
    img VARCHAR(500) NOT NULL,
    category ENUM(
        'breakfast',
        'lunch',
        'dinner',
        'snack',
        'dessert'
    ) NOT NULL,
    creator_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (creator_id) REFERENCES accounts (id) ON DELETE CASCADE
)

DROP TABLE recipes;

DROP TABLE accounts;

INSERT INTO
    recipes (
        title,
        instructions,
        img,
        category,
        creator_id
    )
VALUES (
        'Spaghetti',
        'Toss tomato sauce with beef and throw in noodles!',
        'https://plus.unsplash.com/premium_photo-1677000666741-17c3c57139a2?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8c3BhZ2V0dGl8ZW58MHx8MHx8fDA%3D&auto=format&fit=crop&q=60&w=500',
        'dinner',
        '68f0074866de1bae805da438'
    ),
    (
        'Pancakes',
        'get pancake premix throw some water in it and boom!',
        'https://plus.unsplash.com/premium_photo-1677000666741-17c3c57139a2?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8c3BhZ2V0dGl8ZW58MHx8MHx8fDA%3D&auto=format&fit=crop&q=60&w=500',
        'breakfast',
        '68f007cfb3779423b1c0387a'
    );

SELECT * FROM recipes

SELECT * FROM accounts

SELECT * FROM accounts WHERE id = '68f0074866de1bae805da438'