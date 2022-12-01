----------------------------------------------------------------------------------------------------------------------------------

CREATE OR REPLACE PROCEDURE registerClient(
	login varchar(100),
	password varchar(100),
	first_name varchar(100),
	last_name varchar(100),
	patronymic varchar(100),
	date_of_birth date
)
LANGUAGE PLPGSQL    
AS $$

DECLARE var uuid;

BEGIN
	SELECT gen_random_uuid() INTO var;
    
	INSERT INTO Users VALUES (var,
							  login,
							  password,
							  first_name,
							  last_name,
						      (SELECT id FROM Roles WHERE role_name = 'client'));

	INSERT INTO Clients VALUES (var,
								patronymic,
								date_of_birth);

    COMMIT;
END;$$

-- CALL registerClient('qwe', 'qwe', 'qwe', 'qwe', 'qwe', '1945-01-01');

----------------------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------------------

CREATE OR REPLACE PROCEDURE createOrder(
	client_id uuid,
	offer_id uuid,
	final_price float8,
	arriving_date date,
	departure_date date
)
LANGUAGE PLPGSQL    
AS $$

DECLARE var uuid;

BEGIN
	SELECT gen_random_uuid() INTO var;
    
	INSERT INTO orders VALUES (var,
							  client_id,
							  offer_id,
							  final_price,
							  arriving_date,
							  departure_date);

    COMMIT;
END;$$

-- DO $$
-- DECLARE client_id_for_order uuid;
-- DECLARE offer_id_for_order uuid;
-- BEGIN
-- 		SELECT id FROM Clients WHERE patronymic = 'qwe' INTO client_id_for_order;
-- 		SELECT id FROM Offers WHERE cost_for_day = 10000 INTO offer_id_for_order;

--     CALL createOrder(client_id_for_order,
-- 					 offer_id_for_order,
-- 					 100500,
-- 					 '1111-11-11',
-- 					 '2222-11-11');
-- END $$;

----------------------------------------------------------------------------------------------------------------------------------

-- SELECT logs.id, logs.client_id , logs.happened_at, logstypes.log_type_name FROM logs
-- JOIN logstypes ON logs.log_type_id = logstypes.id;
