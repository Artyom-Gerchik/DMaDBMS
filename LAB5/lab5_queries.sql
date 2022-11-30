-- DROP TRIGGER order_created ON orders;
-- DROP FUNCTION log_order_created(); 

-- CREATE OR REPLACE FUNCTION log_order_created()
--   RETURNS TRIGGER 
--   LANGUAGE PLPGSQL
--   AS
-- $$
-- BEGIN
-- 		 INSERT INTO logs(id, log_type_id, happened_at, client_id)
-- 		 VALUES(gen_random_uuid(), (SELECT id FROM logstypes WHERE log_type_name='orderCreated'), now(), NEW.client_id);
		 
-- 	RETURN NEW;
-- END;
-- $$

-- CREATE TRIGGER order_created
--   BEFORE INSERT
--   ON orders
--   FOR EACH ROW
--   EXECUTE PROCEDURE log_order_created();

-- INSERT INTO orders VALUES (gen_random_uuid(),
-- 						   (SELECT id FROM Clients WHERE patronymic = 'bsuir'),
-- 						   (SELECT id FROM Offers WHERE cost_for_day = 12321 AND days = 122),
-- 						   1337,
-- 						   '2010-01-01',
-- 						   '2019-01-11');

-- CREATE OR REPLACE FUNCTION log_client_registrated()
--   RETURNS TRIGGER 
--   LANGUAGE PLPGSQL
--   AS
-- $$
-- BEGIN
-- 		 INSERT INTO logs(id, log_type_id, happened_at, client_id)
-- 		 VALUES(gen_random_uuid(), (SELECT id FROM logstypes WHERE log_type_name='userRegistrated'), now(), NEW.id);
		 
-- 	RETURN NEW;
-- END;
-- $$

-- CREATE TRIGGER client_registarted
--   BEFORE INSERT
--   ON clients
--   FOR EACH ROW
--   EXECUTE PROCEDURE log_client_registrated();

-- INSERT INTO clients VALUES ((SELECT id FROM Users WHERE login = 'test_for_trigger'),
-- 							'test_for_trigger',
-- 							'2000-01-01'
-- 						   );

-- CREATE OR REPLACE FUNCTION log_question_asked()
--   RETURNS TRIGGER 
--   LANGUAGE PLPGSQL
--   AS
-- $$
-- BEGIN
-- 		 INSERT INTO logs(id, log_type_id, happened_at, client_id)
-- 		 VALUES(gen_random_uuid(), (SELECT id FROM logstypes WHERE log_type_name='questionAsked'), now(), NEW.client_id);
		 
-- 	RETURN NEW;
-- END;
-- $$

-- CREATE TRIGGER question_asked
--   BEFORE INSERT
--   ON questions
--   FOR EACH ROW
--   EXECUTE PROCEDURE log_question_asked();

-- INSERT INTO questions VALUES (gen_random_uuid(),
-- 							 (SELECT id FROM Clients WHERE patronymic = 'test_for_trigger'),
-- 							 'test_for_trigger',
-- 							 'test_for_trigger',
-- 							 'true');

-- SELECT logs.id, logs.happened_at, logstypes.log_type_name FROM logs
-- JOIN logstypes ON logs.log_type_id = logstypes.id;
