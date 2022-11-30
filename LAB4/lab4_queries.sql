-----------------------------------------------------------------------------------------------

-- 1. Составлен пул SQL запросов для сложной выборки из БД:
-- Запросы с несколькими условиями.
-- Запросы с вложенными конструкциями.
-- Прочие сложные выборки, необходимые в вашем проекте.


-- SELECT final_price FROM orders WHERE client_id in(
-- 	SELECT id FROM clients WHERE patronymic ILIKE 'patronymic_' AND date_of_birth ='2001-01-01')

-- SELECT final_price FROM orders WHERE offer_id in(
-- 	SELECT id FROM offers WHERE apartments_id in(
-- 		SELECT id FROM apartments WHERE apartments_class_id in(
-- 			SELECT id FROM apartmentsclasses WHERE apartments_class_name = 'Broke')))

-----------------------------------------------------------------------------------------------

-----------------------------------------------------------------------------------------------

-- 2. Составлен пул SQL запросов для получения представлений в БД:
-- JOIN-запросы различных видов (INNER, OUTER, FULL, CROSS, SELF)

-- SELECT orders.id, clients.patronymic, orders.final_price FROM orders
-- 	INNER JOIN clients ON orders.client_id = clients.id;
	
-- SELECT orders.id, orders.final_price, clients.patronymic FROM orders
-- 	LEFT JOIN clients ON orders.final_price = 1111 AND orders.client_id = clients.id;
	
-- SELECT orders.id, orders.final_price, clients.patronymic FROM orders
-- 	RIGHT JOIN clients ON orders.client_id = clients.id;

-- SELECT orders.id, orders.final_price, clients.patronymic FROM orders
-- 	FULL JOIN clients ON orders.final_price = 1111 AND orders.client_id = clients.id;
	
	
-- SELF JOIN --

-- SELECT A.id AS id1, B.id AS id2, A.login
-- FROM users A, users B
-- WHERE A.fk_role_id <> B.fk_role_id 
-- ORDER BY A.fk_role_id;

-- SELF JOIN --

-- CROSS JOIN --

-- SELECT * FROM apartmentsclasses, apartmentstypes;

-- CROSS JOIN -- 

-- MULTIPLE JOIN --

-- SELECT clients.patronymic AS patronymic, orders.id AS order_id, offers.cost_for_day AS cost_for_day FROM clients
-- 	INNER JOIN orders ON orders.client_id = clients.id
-- 		INNER JOIN offers ON offers.id = orders.offer_id

-- MULTIPLE JOIN --

-----------------------------------------------------------------------------------------------

-----------------------------------------------------------------------------------------------

-- 3. Составлен пул SQL запросов для получения сгруппированных данных:
-- GROUP BY + агрегирующие функции
-- HAVING
-- UNION

-- SELECT orders.client_id,
-- 	(SELECT clients.patronymic FROM clients WHERE id = orders.client_id) AS patronymic,
-- 		SUM(final_price) AS money_spent
-- 			FROM orders GROUP BY orders.client_id;

-- SELECT orders.client_id,
-- 	(SELECT clients.patronymic FROM clients WHERE id = orders.client_id) AS patronymic,
-- 		SUM(final_price) AS money_spent
-- 			FROM orders GROUP BY orders.client_id
-- 				HAVING SUM(final_price) > 199;

-- SELECT offer_id FROM orders
-- UNION 
-- SELECT id FROM offers;

-----------------------------------------------------------------------------------------------

-----------------------------------------------------------------------------------------------

-- 4. Составлен пул SQL запросов для сложных операций с данными:
-- EXISTS
-- INSERT INTO SELECT
-- CASE
-- EXPLAIN 

-- SELECT id FROM clients
-- 	WHERE EXISTS (SELECT id FROM orders WHERE orders.client_id = clients.id AND orders.final_price >= 199)

-- INSERT INTO offers (id, apartments_id, cost_for_day, days, country, address)
-- 	SELECT id, apartments_id, cost_for_day, days, country, address FROM offers
-- 		WHERE cost_for_day = 1111;

-- SELECT client_id, final_price,
-- CASE
-- 	WHEN final_price = 1111 THEN 'Equals 1111'
-- 	WHEN final_price > 200 THEN 'Greater than 200'
-- 	ELSE 'Less than 200'
	
-- END AS Comprasion
-- FROM orders;

-- EXPLAIN SELECT * FROM orders;

-----------------------------------------------------------------------------------------------
