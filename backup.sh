mysqldump -uroot -p toastmasters > /var/opt/aspnetcore/toastmasters.web/backup.sql
aws s3 cp /var/opt/aspnetcore/toastmasters.web/backup.sql  s3://dougmany/backups
