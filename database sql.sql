
CREATE DATABASE `chnau`;

USE `chnau`;



DROP TABLE IF EXISTS `a_info_table`;

CREATE TABLE `a_info_table` (
  `A_Id` int(11) NOT NULL AUTO_INCREMENT,
  `A_Name` char(100) COLLATE utf8_unicode_ci NOT NULL,
  `A_Url` char(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `A_Status` char(1) COLLATE utf8_unicode_ci DEFAULT 'N',
  PRIMARY KEY (`A_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;



DROP TABLE IF EXISTS `c_info_table`;

CREATE TABLE `c_info_table` (
  `C_Id` int(11) NOT NULL AUTO_INCREMENT,
  `C_Name` char(100) COLLATE utf8_unicode_ci NOT NULL,
  `C_Address` char(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `C_Url` char(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `C_Tel` int(15) DEFAULT NULL,
  `C_Status` char(1) COLLATE utf8_unicode_ci DEFAULT 'N',
  PRIMARY KEY (`C_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;


DROP TABLE IF EXISTS `l_info_table`;

CREATE TABLE `l_info_table` (
  `L_Id` int(11) NOT NULL AUTO_INCREMENT,
  `L_Name` char(100) COLLATE utf8_unicode_ci NOT NULL,
  `L_Date` datetime DEFAULT CURRENT_TIMESTAMP,
  `L_Content` text COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`L_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;



DROP TABLE IF EXISTS `p_info_table`;

CREATE TABLE `p_info_table` (
  `P_Id` int(11) NOT NULL AUTO_INCREMENT,
  `P_Barcode` int(15) NOT NULL,
  `P_Cer_Num` char(22) COLLATE utf8_unicode_ci NOT NULL,
  `P_Name` char(40) COLLATE utf8_unicode_ci DEFAULT NULL,
  `P_Weight` float DEFAULT '0',
  `P_Price` int(11) DEFAULT '0',
  `P_Standard` char(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `P_Category` char(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `P_C_Key` int(11) DEFAULT NULL,
  `P_T_key` int(11) DEFAULT NULL,
  `P_A_Key` int(11) DEFAULT NULL,
  `P_Remarks` text COLLATE utf8_unicode_ci,
  `P_Status` char(1) COLLATE utf8_unicode_ci DEFAULT 'N',
  PRIMARY KEY (`P_Id`,`P_Barcode`,`P_Cer_Num`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;



DROP TABLE IF EXISTS `u_info_table`;

CREATE TABLE `u_info_table` (
  `U_Id` int(11) NOT NULL AUTO_INCREMENT,
  `U_Name` char(100) COLLATE utf8_unicode_ci NOT NULL,
  `U_Password` char(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `U_Status` char(1) COLLATE utf8_unicode_ci DEFAULT 'N',
  PRIMARY KEY (`U_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;


