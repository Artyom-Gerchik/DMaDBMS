-- SELECT * FROM Roles
-- INSERT INTO Roles VALUES (gen_random_uuid(), 'client');
-- INSERT INTO Roles VALUES (gen_random_uuid(), 'admin');
-- INSERT INTO Roles VALUES (gen_random_uuid(), 'moderator');

-- SELECT * FROM Users
-- INSERT INTO Users VALUES (gen_random_uuid(),
-- 						  'login1',
-- 						  'password1',
-- 						  'first_name1',
-- 						  'last_name1',
-- 						  (SELECT id FROM Roles WHERE role_name = 'client'));
-- INSERT INTO Users VALUES (gen_random_uuid(),
-- 						  'login2',
-- 						  'password2',
-- 						  'first_name2',
-- 						  'last_name2',
-- 						  (SELECT id FROM Roles WHERE role_name = 'client'));
-- INSERT INTO Users VALUES (gen_random_uuid(),
-- 						  'login3',
-- 						  'password3',
-- 						  'first_name3',
-- 						  'last_name3',
-- 						  (SELECT id FROM Roles WHERE role_name = 'client'));
-- INSERT INTO Users VALUES (gen_random_uuid(),
-- 						  'login4',
-- 						  'password4',
-- 						  'first_name4',
-- 						  'last_name4',
-- 						  (SELECT id FROM Roles WHERE role_name = 'admin'));
-- INSERT INTO Users VALUES (gen_random_uuid(),
-- 						  'login5',
-- 						  'password5',
-- 						  'first_name5',
-- 						  'last_name5',
-- 						  (SELECT id FROM Roles WHERE role_name = 'moderator'));

-- SELECT * FROM Clients
-- INSERT INTO Clients VALUES ((SELECT id FROM Users WHERE login = 'login1'),
-- 							'patronymic1',
-- 							'2001-01-01'
-- 						   );
-- INSERT INTO Clients VALUES ((SELECT id FROM Users WHERE login = 'login2'),
-- 							'patronymic2',
-- 							'2002-02-02'
-- 						   );
-- INSERT INTO Clients VALUES ((SELECT id FROM Users WHERE login = 'login3'),
-- 							'patronymic3',
-- 							'2003-03-03'
-- 						   );

-- SELECT * FROM Administrators
-- INSERT INTO Administrators VALUES ((SELECT id FROM Users WHERE login = 'login4'));

-- SELECT * FROM Moderators
-- INSERT INTO Moderators VALUES ((SELECT id FROM Users WHERE login = 'login5'));

-- SELECT * FROM PrizeDraws
-- INSERT INTO PrizeDraws VALUES (gen_random_uuid(), 100500);

-- SELECT * FROM PrizeDrawsClients
-- INSERT INTO PrizeDrawsClients VALUES ((SELECT id FROM PrizeDraws WHERE amount_of_money = 100500),
-- 									  (SELECT id FROM Clients WHERE patronymic = 'patronymic1'));
-- INSERT INTO PrizeDrawsClients VALUES ((SELECT id FROM PrizeDraws WHERE amount_of_money = 100500),
-- 									  (SELECT id FROM Clients WHERE patronymic = 'patronymic2'));

-- SELECT * FROM Questions
-- INSERT INTO Questions VALUES (gen_random_uuid(),
-- 							 (SELECT id FROM Clients WHERE patronymic = 'patronymic1'),
-- 							 'question1',
-- 							 'answer1',
-- 							 'true');
-- INSERT INTO Questions VALUES (gen_random_uuid(),
-- 							 (SELECT id FROM Clients WHERE patronymic = 'patronymic1'),
-- 							 'question2',
-- 							 '',
-- 							 'false');							 

-- SELECT * FROM LogsTypes
-- INSERT INTO LogsTypes VALUES (gen_random_uuid(),
-- 							  'log_type1');
-- INSERT INTO LogsTypes VALUES (gen_random_uuid(),
-- 							  'log_type2');
-- INSERT INTO LogsTypes VALUES (gen_random_uuid(),
-- 							  'log_type3');

-- SELECT * FROM Logs
-- INSERT INTO Logs VALUES (gen_random_uuid(),
-- 						 (SELECT id FROM LogsTypes WHERE log_type_name = 'log_type1'));
-- INSERT INTO Logs VALUES (gen_random_uuid(),
-- 						 (SELECT id FROM LogsTypes WHERE log_type_name = 'log_type1'));
-- INSERT INTO Logs VALUES (gen_random_uuid(),
-- 						 (SELECT id FROM LogsTypes WHERE log_type_name = 'log_type2'));
-- INSERT INTO Logs VALUES (gen_random_uuid(),
-- 						 (SELECT id FROM LogsTypes WHERE log_type_name = 'log_type3'));

-- SELECT * FROM ApartmentsTypes
-- INSERT INTO ApartmentsTypes VALUES (gen_random_uuid(),
-- 							  		'flat');
-- INSERT INTO ApartmentsTypes VALUES (gen_random_uuid(),
-- 							  		'house');
									
-- SELECT * FROM ApartmentsClasses		
-- INSERT INTO ApartmentsClasses VALUES (gen_random_uuid(),
-- 							  		  'Premium');
-- INSERT INTO ApartmentsClasses VALUES (gen_random_uuid(),
-- 							  		  'Broke');

-- SELECT * FROM Apartments
-- INSERT INTO Apartments VALUES (gen_random_uuid(),
-- 							   (SELECT id FROM ApartmentsTypes WHERE apartments_type_name = 'flat'),
-- 							   4,
-- 							   1,
-- 							   5,
-- 							   (SELECT id FROM ApartmentsClasses WHERE apartments_class_name = 'Premium'));
-- INSERT INTO Apartments VALUES (gen_random_uuid(),
-- 							   (SELECT id FROM ApartmentsTypes WHERE apartments_type_name = 'house'),
-- 							   1,
-- 							   1,
-- 							   2,
-- 							   (SELECT id FROM ApartmentsClasses WHERE apartments_class_name = 'Broke'));

-- SELECT * FROM Offers
-- INSERT INTO Offers VALUES (gen_random_uuid(),
-- 						   (SELECT id FROM Apartments WHERE apartments_type_id = (SELECT id FROM ApartmentsTypes WHERE apartments_type_name = 'flat')),
-- 						   10000,
-- 						   10,
-- 						   'Belarus',
-- 						   'Dzerjinskogo district, 11-119');

-- SELECT * FROM Orders
-- INSERT INTO Orders VALUES (gen_random_uuid(),
-- 						   (SELECT id FROM Clients WHERE patronymic = 'patronymic1'),
-- 						   (SELECT id FROM Offers WHERE cost_for_day = 10000),
-- 						   100000,
-- 						   '2010-01-01',
-- 						   '2010-01-11');