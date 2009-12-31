insert into events select Id, ConferenceKey, Name, Description, StartDate, EndDate, LocationName, LocationUrl,Address, City, Region, PostalCode , TimeZone , UserGroupID from conference_migration
insert into meetings select Id, '', '', '', '', '' from conference_migration
