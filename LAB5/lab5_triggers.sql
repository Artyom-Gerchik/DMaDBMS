-- DROP TRIGGER order_created ON orders;
-- DROP FUNCTION log_order_created(); 

----------------------------------------------------------------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION log_order_created()
  RETURNS TRIGGER 
  LANGUAGE PLPGSQL
  AS
$$
BEGIN
		 INSERT INTO logs(id, log_type_id, happened_at, client_id)
		 VALUES(gen_random_uuid(), (SELECT id FROM logstypes WHERE log_type_name='orderCreated'), now(), NEW.client_id);
		 
	RETURN NEW;
END;
$$

CREATE TRIGGER order_created
  BEFORE INSERT
  ON orders
  FOR EACH ROW
  EXECUTE FUNCTION log_order_created();

-- INSERT INTO orders VALUES (gen_random_uuid(),
-- 						   (SELECT id FROM Clients WHERE patronymic = 'bsuir'),
-- 						   (SELECT id FROM Offers WHERE cost_for_day = 12321 AND days = 122),
-- 						   1337,
-- 						   '2010-01-01',
-- 						   '2019-01-11');

----------------------------------------------------------------------------------------------------------------------------------

-- DROP TRIGGER client_registarted ON clients;
-- DROP FUNCTION log_client_registrated(); 

----------------------------------------------------------------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION log_client_registrated()
  RETURNS TRIGGER 
  LANGUAGE PLPGSQL
  AS
$$
BEGIN
		 INSERT INTO logs(id, log_type_id, happened_at, client_id)
		 VALUES(gen_random_uuid(), (SELECT id FROM logstypes WHERE log_type_name='clientRegistrated'), now(), NEW.id);
		 
	RETURN NEW;
END;
$$

CREATE TRIGGER client_registarted
  BEFORE INSERT
  ON clients
  FOR EACH ROW
  EXECUTE FUNCTION log_client_registrated();

-- INSERT INTO Users VALUES (gen_random_uuid(),
-- 						  'aab',
-- 						  'aa',
-- 						  'aa',
-- 						  'aa',
-- 						  (SELECT id FROM Roles WHERE role_name = 'client'));

-- INSERT INTO clients VALUES ((SELECT id FROM Users WHERE login = 'aab'),
-- 							'test_for_trigger',
-- 							'2000-01-01'
-- 						   );

----------------------------------------------------------------------------------------------------------------------------------


-- DROP TRIGGER question_asked ON questions;
-- DROP FUNCTION log_question_asked(); 

----------------------------------------------------------------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION log_question_asked()
  RETURNS TRIGGER 
  LANGUAGE PLPGSQL
  AS
$$
BEGIN
		 INSERT INTO logs(id, log_type_id, happened_at, client_id)
		 VALUES(gen_random_uuid(), (SELECT id FROM logstypes WHERE log_type_name='questionAsked'), now(), NEW.client_id);
		 
	RETURN NEW;
END;
$$

CREATE TRIGGER question_asked
  BEFORE INSERT
  ON questions
  FOR EACH ROW
  EXECUTE FUNCTION log_question_asked();

-- INSERT INTO questions VALUES (gen_random_uuid(),
-- 							 (SELECT id FROM Users WHERE login = 'aa'),
-- 							 'test_for_trigger',
-- 							 'test_for_trigger',
-- 							 'true');

----------------------------------------------------------------------------------------------------------------------------------


-- TRUNCATE TABLE logs

-- SELECT logs.id, logs.client_id , logs.happened_at, logstypes.log_type_name FROM logs
-- JOIN logstypes ON logs.log_type_id = logstypes.id;