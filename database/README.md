# SQL Test Assignment

Attached is a mysqldump of a database to be used during the test.

Below are the questions for this test. Please enter a full, complete, working SQL statement under each question. We do not want the answer to the question. We want the SQL command to derive the answer. We will copy/paste these commands to test the validity of the answer.

**Example:**

_Q. Select all users_

- Please return at least first_name and last_name

SELECT first_name, last_name FROM users;


------

**— Test Starts Here —**

1. Select users whose id is either 3,2 or 4
- Please return at least: all user fields
SELECT * FROM users WHERE Id BETWEEN 2 AND 4

2. Count how many basic and premium listings each active user has
- Please return at least: first_name, last_name, basic, premium


3. Show the same count as before but only if they have at least ONE premium listing
- Please return at least: first_name, last_name, basic, premium


4. How much revenue has each active vendor made in 2013
- Please return at least: first_name, last_name, currency, revenue


5. Insert a new click for listing id 3, at $4.00
- Find out the id of this new click. Please return at least: id

Insert query;
INSERT INTO clicks (listing_id, price, currency)
VALUES (3,4.00,'USD');

Query to find it:
SELECT * FROM clicks 
WHERE listing_id = 3 AND price = 4.00 
AND currency = 'USD' AND created IS NULL

6. Show listings that have not received a click in 2013
- Please return at least: listing_name
SELECT id, name as listing_name  FROM listings l
WHERE l.id NOT IN (SELECT DISTINCT listing_id FROM clicks c WHERE created <= '2013-12-31' and created >= '2013-01-01');


7. For each year show number of listings clicked and number of vendors who owned these listings
- Please return at least: date, total_listings_clicked, total_vendors_affected

SELECT c.created as date, SUM(c.listing_id) as total_listings, SUM(u.id) as total_vendors
FROM clicks c 
INNER JOIN listings l ON c.listing_id = l.id
INNER JOIN users u ON l.user_id = u.id
GROUP BY c.created

8. Return a comma separated string of listing names for all active vendors
- Please return at least: first_name, last_name, listing_names

SELECT CONCAT_WS(', ', u.first_name, u.last_name, l.name)
FROM listings l INNER JOIN users u ON l.user_id = u.id
WHERE u.status = 2