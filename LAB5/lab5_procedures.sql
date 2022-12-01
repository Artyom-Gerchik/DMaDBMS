-- CREATE OR REPLACE PROCEDURE registerClient(
-- 	login varchar(100),
-- 	password varchar(100),
-- 	first_name varchar(100),
-- 	last_name varchar(100),
-- 	patronymic varchar(100),
-- 	date_of_birth date
-- )
-- LANGUAGE PLPGSQL    
-- AS $$

-- DECLARE var uuid;

-- BEGIN
-- 	SELECT gen_random_uuid() INTO var;
    
-- 	INSERT INTO Users VALUES (var,
-- 							  login,
-- 							  password,
-- 							  first_name,
-- 							  last_name,
-- 						      (SELECT id FROM Roles WHERE role_name = 'client'));

-- 	INSERT INTO Clients VALUES (var,
-- 								patronymic,
-- 								date_of_birth);

--     COMMIT;
-- END;$$

-- CALL registerClient('qwe', 'qwe', 'qwe', 'qwe', 'qwe', '1945-01-01');

SELECT logs.id, logs.client_id , logs.happened_at, logstypes.log_type_name FROM logs
JOIN logstypes ON logs.log_type_id = logstypes.id;
