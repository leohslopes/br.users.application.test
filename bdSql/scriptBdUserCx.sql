CREATE DATABASE  IF NOT EXISTS `users_test_cx` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `users_test_cx`;
-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: localhost    Database: users_test_cx
-- ------------------------------------------------------
-- Server version	8.0.41

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `users_cx`
--

DROP TABLE IF EXISTS `users_cx`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users_cx` (
  `id_user` int NOT NULL AUTO_INCREMENT,
  `name_user` varchar(30) NOT NULL,
  `email_user` varchar(50) NOT NULL,
  `age_user` int DEFAULT NULL,
  `gender_user` char(1) DEFAULT NULL,
  `password_user` varchar(200) NOT NULL,
  PRIMARY KEY (`id_user`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users_cx`
--

LOCK TABLES `users_cx` WRITE;
/*!40000 ALTER TABLE `users_cx` DISABLE KEYS */;
INSERT INTO `users_cx` VALUES (1,'Leonardo Silverio Lopes','leo_sil117@hotmail.com',27,'M','AQAAAAIAAYagAAAAEPULNGNeYNimZ7ppsQBV/xGA3gHWgwoxl+5whY6bJnxUIvZnzZv0MHuuWnbmjQ6C2g=='),(2,'Felipe Mário Kronemberger','felipe.0785@gmail.com',40,'M','AQAAAAIAAYagAAAAEPULNGNeYNimZ7ppsQBV/xGA3gHWgwoxl+5whY6bJnxUIvZnzZv0MHuuWnbmjQ6C2g=='),(3,'Patricia Carriel Silverio','paty181@hotmail.com',53,'F','AQAAAAIAAYagAAAAEPULNGNeYNimZ7ppsQBV/xGA3gHWgwoxl+5whY6bJnxUIvZnzZv0MHuuWnbmjQ6C2g=='),(4,'Sueli Maria Carriel','sueli.carriel2@gmail.com',72,'F','AQAAAAIAAYagAAAAEPULNGNeYNimZ7ppsQBV/xGA3gHWgwoxl+5whY6bJnxUIvZnzZv0MHuuWnbmjQ6C2g=='),(5,'Teste','test@mail.com',5,'M','AQAAAAIAAYagAAAAEPULNGNeYNimZ7ppsQBV/xGA3gHWgwoxl+5whY6bJnxUIvZnzZv0MHuuWnbmjQ6C2g=='),(6,'Dannylo Carlos','dann.carlos@outlook.com',27,'M','AQAAAAIAAYagAAAAEPULNGNeYNimZ7ppsQBV/xGA3gHWgwoxl+5whY6bJnxUIvZnzZv0MHuuWnbmjQ6C2g=='),(7,'Ivan Rodrigues','ivan.rodrigues@gmail.com',31,'M','AQAAAAIAAYagAAAAEPULNGNeYNimZ7ppsQBV/xGA3gHWgwoxl+5whY6bJnxUIvZnzZv0MHuuWnbmjQ6C2g=='),(8,'Paula Fernanda Carriel','paulafernanda.cssoares@gmail.com',41,'F','AQAAAAIAAYagAAAAENkWQOoJ17pa7PYxdvou/mBYYxkPjf5UqVLCsmPTfXBiLSTFQ5SUEy0y8xySrDEsNA==');
/*!40000 ALTER TABLE `users_cx` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-07-13 14:03:41
