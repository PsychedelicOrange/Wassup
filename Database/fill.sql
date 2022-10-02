-- schema tables
create table USERS (UUID int,NAME varchar(10),PRIMARY KEY(UUID));
create table ROOMS (RUID int,NAME varchar(21),ISGROUP int,PRIMARY KEY(RUID));
create table USER_ROOM (UUID int,RUID int,ISGROUP int,FOREIGN KEY (UUID) REFERENCES USERS(UUID),FOREIGN KEY (RUID) REFERENCES ROOMS(RUID));
create table MESSAGES (UUID int,RUID int,ISGROUP int,time TIMESTAMP,CONTENT varchar(100),FOREIGN KEY (UUID) REFERENCES USERS(UUID),FOREIGN KEY (RUID) REFERENCES ROOMS(RUID),PRIMARY KEY (UUID,RUID,time));
create table BANKACC (UUID int,balance float,FOREIGN KEY (UUID) REFERENCES USERS(UUID),PRIMARY KEY (UUID,balance));
create table TRANSACS (sendeUUIDrUUID int,recieverUUID int,amount float);
---- INSERTING DATA

-- create users
insert into USERS values(0,'Parth');
insert into USERS values(1,'Arihant');
insert into USERS values(3,'Kushal');

-- create bank accounts for users
insert into BANKACC values(0,10000);
insert into BANKACC values(1,10000);
insert into BANKACC values(2,10000);

-- create room DM
insert into ROOMS values(0,'Parth-Arihant',0);
-- create room group 
insert into ROOMS values(1,'Sigma',1);
insert into ROOMS values(2,'ITB',1);
-- add person to DM
insert into USER_ROOM values(0,0,0);
insert into USER_ROOM values(1,0,0);
-- add people to group
CREATE OR REPLACE addUser(userid in INT,roomid in INT)
AS 
BEGIN
IF (select ISGROUP from Rooms where RUID = roomid) = 1
    insert into USER_ROOM values(userid,roomid,1);
ELSE
    insert into USER_ROOM values(userid,roomid,1);
END IF;
insert into USER_ROOM values(1,1,1);
insert into USER_ROOM values(1,2,1);
insert into USER_ROOM values(0,2,1);
insert into USER_ROOM values(0,1,1);
insert into USER_ROOM values(3,2,1);
-- send message // TO DO GET ROOM ID FROM DM !!! 
CREATE OR REPLACE PROCEDURE sendMsg(sender IN int,roomid IN int,msg IN string)
AS 
BEGIN
IF (select ISGROUP from Rooms where RUID = roomid) = 1
    insert into MESSAGES values(sender,roomid,1,sysdate,msg);
ELSE
    insert into MESSAGES values(sender,roomid,0,sysdate,msg);
END IF;
-- sendMoney(sender,reciever,amount);
CREATE OR REPLACE PROCEDURE sendMoney(sender IN int ,reciever IN int , amount in float)
AS
BEGIN
    insert into TRANSACS values(sender,reciever,69);
    update BANKACC SET balance = balance - amount WHERE UUID = sender; 
    update BANKACC SET balance = balance + amount WHERE UUID = reciever;
END;
/
---- SELECTING DATA

-- rooms a person belongs to
select * from ROOMS where RUID in ( select RUID from USER_ROOM where UUID = 2);


