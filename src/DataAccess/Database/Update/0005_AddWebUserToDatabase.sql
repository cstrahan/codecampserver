CREATE USER [webuser] FOR LOGIN [webuser] WITH DEFAULT_SCHEMA=[dbo]
GO
grant insert on events to webuser
grant select on events to webuser
grant update on events to webuser
grant delete on events to webuser

grant insert on attendees to webuser
grant select on attendees to webuser
grant update on attendees to webuser
grant delete on attendees to webuser
go