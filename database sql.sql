
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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `c_info_table` */

insert  into `c_info_table`(`C_Id`,`C_Name`,`C_Address`,`C_Url`,`C_Tel`,`C_Status`) values 

(1,'福鑫珠宝城','偃师市华夏路中段华夏广场对面',NULL,'0379-67688888','N');

/*Table structure for table `l_info_table` */

DROP TABLE IF EXISTS `l_info_table`;

CREATE TABLE `l_info_table` (
  `L_Id` int(11) NOT NULL AUTO_INCREMENT,
  `L_Name` char(100) COLLATE utf8_unicode_ci NOT NULL,
  `L_Date` datetime DEFAULT CURRENT_TIMESTAMP,
  `L_Content` text COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`L_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;


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
) ENGINE=InnoDB AUTO_INCREMENT=173 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `p_info_table` */

/*Table structure for table `t_info_table` */

DROP TABLE IF EXISTS `t_info_table`;

CREATE TABLE `t_info_table` (
  `T_Id` int(11) NOT NULL AUTO_INCREMENT,
  `T_Name` char(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `T_Url` char(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `T_Tel` char(15) COLLATE utf8_unicode_ci DEFAULT NULL,
  `T_Status` char(1) COLLATE utf8_unicode_ci DEFAULT 'N',
  PRIMARY KEY (`T_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

/*Data for the table `t_info_table` */

insert  into `t_info_table`(`T_Id`,`T_Name`,`T_Url`,`T_Tel`,`T_Status`) values 

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

(3,'sa','111111','N','2018-11-27 02:01:00')

