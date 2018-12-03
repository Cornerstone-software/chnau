
CREATE DATABASE /*!32312 IF NOT EXISTS*/`chnau` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_ci */;

USE `chnau`;

/*Table structure for table `a_info_table` */

DROP TABLE IF EXISTS `a_info_table`;

CREATE TABLE `a_info_table` (
  `A_Id` int(11) NOT NULL AUTO_INCREMENT,
  `A_Name` char(100) COLLATE utf8_unicode_ci NOT NULL,
  `A_Url` char(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `A_Status` char(1) COLLATE utf8_unicode_ci DEFAULT 'N',
  PRIMARY KEY (`A_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `a_info_table` */

/*Table structure for table `c_info_table` */

DROP TABLE IF EXISTS `c_info_table`;

CREATE TABLE `c_info_table` (
  `C_Id` int(11) NOT NULL AUTO_INCREMENT,
  `C_Name` char(100) COLLATE utf8_unicode_ci NOT NULL,
  `C_Address` char(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `C_Url` char(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `C_Tel` char(15) COLLATE utf8_unicode_ci DEFAULT NULL,
  `C_Status` char(1) COLLATE utf8_unicode_ci DEFAULT 'N',
  PRIMARY KEY (`C_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `c_info_table` */

/*Table structure for table `l_info_table` */

DROP TABLE IF EXISTS `l_info_table`;

CREATE TABLE `l_info_table` (
  `L_Id` int(11) NOT NULL AUTO_INCREMENT,
  `L_Name` char(100) COLLATE utf8_unicode_ci NOT NULL,
  `L_Date` datetime DEFAULT CURRENT_TIMESTAMP,
  `L_Content` text COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`L_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `l_info_table` */

insert  into `l_info_table`(`L_Id`,`L_Name`,`L_Date`,`L_Content`) values 

(1,'gerena','2018-11-27 23:58:32','addUser'),

(2,'gerena','2018-11-27 23:58:55','addUser'),

(3,'gerena','2018-11-27 23:59:30','addUser'),

(4,'gerena','2018-11-28 00:01:46','addUser');

/*Table structure for table `p_info_table` */

DROP TABLE IF EXISTS `p_info_table`;

CREATE TABLE `p_info_table` (
  `P_Id` int(11) NOT NULL AUTO_INCREMENT,
  `P_Barcode` char(15) COLLATE utf8_unicode_ci NOT NULL,
  `P_CerNum` char(25) COLLATE utf8_unicode_ci NOT NULL,
  `P_Name` char(40) COLLATE utf8_unicode_ci DEFAULT NULL,
  `P_Weight` float DEFAULT '0',
  `P_Price` int(11) DEFAULT '0',
  `P_Standard` char(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `P_Category` char(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `P_CId` int(11) DEFAULT NULL,
  `P_TId` int(11) DEFAULT NULL,
  `P_AId` int(11) DEFAULT NULL,
  `P_UName` char(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `P_Remarks` text COLLATE utf8_unicode_ci,
  `P_Date` datetime DEFAULT CURRENT_TIMESTAMP,
  `P_Status` char(1) COLLATE utf8_unicode_ci DEFAULT 'N',
  PRIMARY KEY (`P_Id`,`P_Barcode`,`P_CerNum`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `p_info_table` */

insert  into `p_info_table`(`P_Id`,`P_Barcode`,`P_CerNum`,`P_Name`,`P_Weight`,`P_Price`,`P_Standard`,`P_Category`,`P_CId`,`P_TId`,`P_AId`,`P_UName`,`P_Remarks`,`P_Date`,`P_Status`) values 

(22,'19080110290','0371-79180301010501-0272','足金戒指',5.33,1700,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 12:11:09','N'),

(23,'19080110291','0371-79180301010501-0273','足金戒指',6.33,2000,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 13:11:52','N'),

(24,'19080110292','0371-79180301010501-0274','足金戒指',7.33,2350,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 13:11:53','N'),

(25,'19080110293','0371-79180301010501-0275','足金戒指',8.33,2700,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 13:11:53','N'),

(26,'19080110294','0371-79180301010501-0276','足金戒指',9.33,3000,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 13:11:53','N'),

(27,'19080110295','0371-79180301010501-0277','足金戒指',10.33,30300,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 13:11:53','N'),

(28,'19080110296','0371-79180301010501-0278','足金戒指',11.33,30600,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 13:11:53','N'),

(29,'19080110297','0371-79180301010501-0279','足金戒指',11.33,30600,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 13:28:34','N'),

(30,'19080110298','0371-79180301010501-0280','足金戒指',5.33,1700,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 13:46:27','N'),

(31,'19080110299','0371-79180301010501-0281','足金戒指',11.33,30600,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 15:09:03','N'),

(32,'19080110300','0371-79180301010501-0282','足金戒指',11.33,30600,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 15:09:03','N'),

(33,'19080110301','0371-79180301010501-0283','足金戒指',11.33,30600,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 15:09:03','N'),

(34,'19080110302','0371-79180301010501-0284','足金戒指',11.33,30600,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 15:09:03','N'),

(35,'19080110303','0371-79180301010501-0285','足金戒指',11.33,30600,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 15:09:04','N'),

(36,'19080110304','0371-79180301010501-0286','足金戒指',11.33,30600,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 15:09:04','N'),

(37,'19080110305','0371-79180301010501-0287','足金戒指',11.33,30600,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 15:09:04','N'),

(38,'19080110306','0371-79180301010501-0288','足金戒指',11.33,30600,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 15:09:04','N'),

(39,'19080110307','0371-79180301010501-0289','足金戒指',11.33,30600,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 15:09:04','N'),

(40,'19080110308','0371-79180301010501-0290','足金戒指',11.33,30600,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 15:09:04','N'),

(41,'19080110309','0371-79180301010501-0291','足金戒指',11.33,30600,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 15:09:04','N'),

(42,'19080110310','0371-79180301010501-0292','足金戒指',11.33,30600,'Q/ZJZB 001-2015','黄金',1,1,NULL,NULL,NULL,'2018-12-03 15:09:04','N');

/*Table structure for table `t_info_table` */

DROP TABLE IF EXISTS `t_info_table`;

CREATE TABLE `t_info_table` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` char(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Url` char(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Tel` char(15) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Status` char(1) COLLATE utf8_unicode_ci DEFAULT 'N',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `t_info_table` */

insert  into `t_info_table`(`Id`,`Name`,`Url`,`Tel`,`Status`) values 

(1,'河南省金银珠宝饰品质量监督检验中心','http://www.hntc.cc','4006-567-900','N');

/*Table structure for table `u_info_table` */

DROP TABLE IF EXISTS `u_info_table`;

CREATE TABLE `u_info_table` (
  `U_Id` int(11) NOT NULL AUTO_INCREMENT,
  `U_Name` char(100) COLLATE utf8_unicode_ci NOT NULL,
  `U_Password` char(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `U_Status` char(1) COLLATE utf8_unicode_ci DEFAULT 'N',
  `U_Date` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`U_Id`,`U_Name`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `u_info_table` */

insert  into `u_info_table`(`U_Id`,`U_Name`,`U_Password`,`U_Status`,`U_Date`) values 

(1,'admin','admin','N','0000-00-00 00:00:00'),

(3,'sa','111111','N','2018-11-27 02:01:00'),

(4,'ta','123456','N',NULL),

(5,'ua','123456','N',NULL),

(6,'aa','123456','N',NULL),

(10,'yoyo','123456','N',NULL),

(11,'anna','123456','N',NULL),

(27,'nana','111111','N',NULL),

(28,'goto','888888','N',NULL),

(29,'gerena','123456','N',NULL);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
