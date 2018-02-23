mysqldump toastmasters > /var/opt/aspnetcore/toastmasters.web/backup.sql
mysqldump toastmastersTraining > /var/opt/aspnetcore/toastmasters.web.training/backup.sql
mysqldump toastmastersTrashTalkers > /var/opt/aspnetcore/toastmasters.trashtalkers.web/backup.sql

aws s3 cp /var/opt/aspnetcore/toastmasters.web/backup.sql  s3://dougmany/backups/backup.sql
aws s3 cp /var/opt/aspnetcore/toastmasters.trashtalkers.web/backup.sql  s3://dougmany/backups/trashtalkers.backup.sql
