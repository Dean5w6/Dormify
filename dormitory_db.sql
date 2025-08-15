-- MariaDB dump 10.19  Distrib 10.4.32-MariaDB, for Win64 (AMD64)
--
-- Host: localhost    Database: dormitory_db
-- ------------------------------------------------------
-- Server version	10.4.32-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `activity_logs`
--

DROP TABLE IF EXISTS `activity_logs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `activity_logs` (
  `log_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) DEFAULT NULL,
  `action_description` varchar(255) NOT NULL,
  `log_timestamp` timestamp NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`log_id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `activity_logs_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `activity_logs`
--

LOCK TABLES `activity_logs` WRITE;
/*!40000 ALTER TABLE `activity_logs` DISABLE KEYS */;
INSERT INTO `activity_logs` VALUES (1,1,'Created new user: mgr_maricel','2025-07-07 05:37:32'),(2,1,'Deleted user: tenant_test (ID: 5).','2025-07-24 15:04:49'),(3,2,'Processed booking ID 36 with status: Approved','2025-07-26 06:34:26'),(4,2,'Processed booking ID 34 with status: Rejected','2025-07-26 06:41:22'),(5,1,'Manually removed tenant from room ID 8.','2025-07-26 07:03:19'),(6,1,'Manually assigned tenant ID 3 to room ID 2.','2025-07-26 07:03:50'),(7,2,'Manually removed tenant from room ID 2.','2025-07-26 07:20:43'),(8,2,'Manually assigned tenant ID 4 to room ID 1.','2025-07-26 07:24:12'),(9,2,'Manually removed tenant from room ID 1.','2025-07-26 07:24:18'),(10,2,'Processed booking ID 37 with status: Rejected','2025-07-26 08:13:57'),(11,2,'Manually assigned tenant ID 4 to room ID 1.','2025-07-26 08:17:09'),(12,2,'Deleted unpaid bill ID: 25.','2025-07-26 08:24:15'),(13,2,'Manually assigned tenant ID 4 to room ID 1.','2025-07-26 10:03:59'),(14,2,'Manually removed tenant ID 4 from room ID 1.','2025-07-26 10:30:42'),(15,2,'Deleted unpaid bill ID: 26.','2025-07-26 10:47:35'),(16,2,'Processed booking ID 43 with status: Rejected','2025-07-26 10:47:44'),(17,2,'Processed booking ID 44 with status: Approved','2025-07-26 10:53:30'),(18,2,'Automatically rejected other pending requests for tenant ID: 0.','2025-07-26 10:53:30'),(19,2,'Processed booking ID 45 with status: Approved','2025-07-26 10:53:36'),(20,2,'Automatically rejected pending requests for now-full room ID: 8.','2025-07-26 10:53:36'),(21,2,'Automatically rejected other pending requests for tenant ID: 0.','2025-07-26 10:53:36'),(22,2,'Manually removed tenant ID 3 from room ID 8.','2025-07-26 10:54:05'),(23,2,'Manually removed tenant ID 4 from room ID 8.','2025-07-26 10:54:14'),(24,2,'Processed booking ID 46 with status: Rejected','2025-07-26 11:07:30'),(25,2,'Manually assigned tenant ID 4 to room ID 3.','2025-07-26 11:08:26'),(26,2,'Completed old booking 47 for tenant 4 as part of a room transfer.','2025-07-26 11:08:32'),(27,2,'Manually assigned tenant ID 4 to room ID 3.','2025-07-26 11:08:32'),(28,2,'Completed old booking 48 for tenant 4 as part of a room transfer.','2025-07-26 11:08:42'),(29,2,'Manually assigned tenant ID 4 to room ID 5.','2025-07-26 11:08:43'),(30,1,'Updated user details for: m3lfx.','2025-07-27 07:28:24'),(31,1,'Updated user details for: m3lfx.','2025-07-27 07:28:49'),(32,1,'Updated user details for: deanfx.','2025-07-27 07:36:01'),(33,1,'Updated user details for: deanfx.','2025-07-27 07:36:10'),(34,2,'Processed booking ID 50 with status: Rejected','2025-07-27 12:55:08'),(35,3,'Tenant ID 3 requested to book room ID 1.','2025-07-27 12:58:37'),(36,2,'Processed booking ID 51 with status: Rejected','2025-07-27 12:59:01'),(37,3,'Tenant ID 3 requested to book room ID 8.','2025-07-27 15:41:05'),(38,6,'Tenant ID 6 requested to book room ID 8.','2025-07-29 05:42:42'),(39,7,'Tenant ID 7 requested to book room ID 8.','2025-07-29 08:07:24'),(40,2,'Processed booking ID 54 with status: Approved','2025-07-29 08:08:17'),(41,2,'Automatically rejected other pending requests for tenant ID: 0.','2025-07-29 08:08:17');
/*!40000 ALTER TABLE `activity_logs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `billings`
--

DROP TABLE IF EXISTS `billings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `billings` (
  `billing_id` int(11) NOT NULL AUTO_INCREMENT,
  `booking_id` int(11) NOT NULL,
  `tenant_id` int(11) NOT NULL,
  `due_date` date NOT NULL,
  `amount_due` decimal(10,2) NOT NULL,
  `status` enum('Unpaid','Paid','Overdue') NOT NULL DEFAULT 'Unpaid',
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`billing_id`),
  KEY `booking_id` (`booking_id`),
  KEY `tenant_id` (`tenant_id`),
  CONSTRAINT `billings_ibfk_1` FOREIGN KEY (`booking_id`) REFERENCES `bookings` (`booking_id`),
  CONSTRAINT `billings_ibfk_2` FOREIGN KEY (`tenant_id`) REFERENCES `users` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `billings`
--

LOCK TABLES `billings` WRITE;
/*!40000 ALTER TABLE `billings` DISABLE KEYS */;
INSERT INTO `billings` VALUES (1,1,3,'2023-09-05',3500.00,'Paid','2025-07-07 05:36:57'),(2,1,3,'2025-07-07',3500.00,'Paid','2025-07-07 11:36:42'),(3,1,3,'2025-07-25',3500.00,'Paid','2025-07-07 11:37:33'),(4,3,3,'2025-07-14',5000.00,'Paid','2025-07-14 14:23:43'),(5,1,3,'2025-07-14',3500.00,'Paid','2025-07-14 14:29:54'),(6,3,3,'2025-07-15',5000.00,'Paid','2025-07-15 00:29:35'),(7,3,3,'2025-07-15',5000.00,'Paid','2025-07-15 00:43:04'),(8,3,3,'2025-07-15',5000.00,'Paid','2025-07-15 00:43:04'),(9,3,3,'2025-07-15',5000.00,'Paid','2025-07-15 00:43:05'),(10,3,3,'2025-12-19',5000.00,'Paid','2025-07-15 00:44:46'),(11,1,3,'2025-12-19',3500.00,'Paid','2025-07-15 00:45:25'),(12,1,3,'2025-12-19',3500.00,'Paid','2025-07-15 00:49:50'),(13,3,3,'2025-07-15',5000.00,'Paid','2025-07-15 01:22:32'),(27,49,4,'2025-07-31',8500.00,'Unpaid','2025-07-26 13:21:43');
/*!40000 ALTER TABLE `billings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bookings`
--

DROP TABLE IF EXISTS `bookings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `bookings` (
  `booking_id` int(11) NOT NULL AUTO_INCREMENT,
  `tenant_id` int(11) NOT NULL,
  `room_id` int(11) NOT NULL,
  `booking_date` timestamp NOT NULL DEFAULT current_timestamp(),
  `move_in_date` date NOT NULL,
  `status` enum('Pending','Approved','Rejected','Completed') NOT NULL DEFAULT 'Pending',
  PRIMARY KEY (`booking_id`),
  KEY `tenant_id` (`tenant_id`),
  KEY `room_id` (`room_id`),
  CONSTRAINT `bookings_ibfk_1` FOREIGN KEY (`tenant_id`) REFERENCES `users` (`user_id`),
  CONSTRAINT `bookings_ibfk_2` FOREIGN KEY (`room_id`) REFERENCES `rooms` (`room_id`)
) ENGINE=InnoDB AUTO_INCREMENT=55 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bookings`
--

LOCK TABLES `bookings` WRITE;
/*!40000 ALTER TABLE `bookings` DISABLE KEYS */;
INSERT INTO `bookings` VALUES (1,3,3,'2025-07-07 05:36:57','2023-08-01','Completed'),(2,4,1,'2025-07-07 05:36:57','2023-09-15','Rejected'),(3,3,2,'2025-07-07 11:11:23','2025-07-07','Completed'),(4,3,1,'2025-07-15 02:40:27','2025-07-15','Rejected'),(5,3,1,'2025-07-15 02:43:37','2025-07-15','Completed'),(6,3,1,'2025-07-15 02:43:39','2025-07-15','Completed'),(7,3,1,'2025-07-15 02:43:41','2025-07-15','Rejected'),(8,3,1,'2025-07-15 02:43:41','2025-07-15','Rejected'),(9,3,1,'2025-07-15 02:43:41','2025-07-15','Rejected'),(10,3,2,'2025-07-15 02:57:03','2025-07-15','Rejected'),(11,3,2,'2025-07-15 02:57:03','2025-07-15','Rejected'),(12,3,2,'2025-07-15 02:57:04','2025-07-15','Rejected'),(13,3,2,'2025-07-15 02:57:04','2025-07-15','Rejected'),(14,3,2,'2025-07-15 02:57:04','2025-07-15','Rejected'),(15,3,2,'2025-07-18 03:05:15','2025-07-18','Rejected'),(16,3,2,'2025-07-18 03:05:17','2025-07-18','Rejected'),(17,3,2,'2025-07-18 03:05:17','2025-07-18','Rejected'),(18,3,2,'2025-07-18 03:05:17','2025-07-18','Rejected'),(19,3,2,'2025-07-18 03:05:20','2025-07-18','Rejected'),(20,3,2,'2025-07-18 03:05:20','2025-07-18','Rejected'),(21,4,8,'2025-07-22 16:19:10','2025-07-23','Rejected'),(22,4,2,'2025-07-22 16:19:46','2025-07-23','Rejected'),(23,4,9,'2025-07-22 16:36:43','2025-07-23','Completed'),(24,4,2,'2025-07-23 13:17:06','2025-07-23','Rejected'),(25,4,8,'2025-07-23 13:59:39','2025-07-23','Rejected'),(26,3,3,'2025-07-23 14:10:58','2025-07-23','Completed'),(27,3,2,'2025-07-23 14:18:36','2025-07-23','Rejected'),(28,3,10,'2025-07-23 14:29:38','2025-07-23','Completed'),(29,4,8,'2025-07-24 14:32:03','2025-07-24','Rejected'),(30,4,8,'2025-07-24 14:37:06','2025-07-24',''),(31,3,8,'2025-07-26 05:53:59','2025-07-26',''),(32,4,10,'2025-07-26 05:54:26','2025-07-26',''),(33,3,8,'2025-07-26 05:56:29','2025-07-26',''),(34,4,8,'2025-07-26 05:57:19','2025-07-26','Rejected'),(35,3,8,'2025-07-26 06:00:09','2025-07-26',''),(36,3,8,'2025-07-26 06:34:09','2025-07-26','Completed'),(37,4,9,'2025-07-26 06:48:34','2025-07-26','Rejected'),(38,3,2,'2025-07-26 07:03:49','2025-07-26','Completed'),(39,4,1,'2025-07-26 07:24:11','2025-07-26','Completed'),(41,4,1,'2025-07-26 08:17:09','2025-07-26','Completed'),(42,4,1,'2025-07-26 10:03:58','2025-07-26','Completed'),(43,3,8,'2025-07-26 10:40:38','2025-07-26','Rejected'),(44,4,8,'2025-07-26 10:41:28','2025-07-26','Completed'),(45,3,8,'2025-07-26 10:49:58','2025-07-26','Completed'),(46,3,8,'2025-07-26 11:07:13','2025-07-26','Rejected'),(47,4,3,'2025-07-26 11:08:25','2025-07-26','Completed'),(48,4,3,'2025-07-26 11:08:32','2025-07-26','Completed'),(49,4,5,'2025-07-26 11:08:42','2025-07-26','Approved'),(50,3,8,'2025-07-27 10:38:23','2025-07-27','Rejected'),(51,3,1,'2025-07-27 12:58:36','2025-07-27','Rejected'),(52,3,8,'2025-07-27 15:41:04','2025-07-27','Pending'),(53,6,8,'2025-07-29 05:42:41','2025-07-29','Pending'),(54,7,8,'2025-07-29 08:07:22','2025-07-29','Approved');
/*!40000 ALTER TABLE `bookings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dormitories`
--

DROP TABLE IF EXISTS `dormitories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `dormitories` (
  `dormitory_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `address` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`dormitory_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dormitories`
--

LOCK TABLES `dormitories` WRITE;
/*!40000 ALTER TABLE `dormitories` DISABLE KEYS */;
INSERT INTO `dormitories` VALUES (1,'Sunshine Residence','122 Maginhawa St, Quezon City'),(2,'Starlight Dorms','456 Taft Ave, Malate, Manila');
/*!40000 ALTER TABLE `dormitories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `expenses`
--

DROP TABLE IF EXISTS `expenses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `expenses` (
  `expense_id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(255) NOT NULL,
  `amount` decimal(10,2) NOT NULL,
  `category` enum('Maintenance','Utilities','Supplies','Other') NOT NULL,
  `expense_date` date NOT NULL,
  `recorded_by` int(11) NOT NULL,
  PRIMARY KEY (`expense_id`),
  KEY `recorded_by` (`recorded_by`),
  CONSTRAINT `expenses_ibfk_1` FOREIGN KEY (`recorded_by`) REFERENCES `users` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `expenses`
--

LOCK TABLES `expenses` WRITE;
/*!40000 ALTER TABLE `expenses` DISABLE KEYS */;
INSERT INTO `expenses` VALUES (1,'Internet',100.00,'Utilities','2025-07-15',1),(2,'Manager Salary',10000.00,'Other','2025-07-15',1);
/*!40000 ALTER TABLE `expenses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payments`
--

DROP TABLE IF EXISTS `payments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `payments` (
  `payment_id` int(11) NOT NULL AUTO_INCREMENT,
  `billing_id` int(11) NOT NULL,
  `amount_paid` decimal(10,2) NOT NULL,
  `payment_date` date NOT NULL,
  `payment_method` varchar(50) DEFAULT NULL,
  `recorded_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`payment_id`),
  KEY `billing_id` (`billing_id`),
  KEY `recorded_by` (`recorded_by`),
  CONSTRAINT `payments_ibfk_1` FOREIGN KEY (`billing_id`) REFERENCES `billings` (`billing_id`),
  CONSTRAINT `payments_ibfk_2` FOREIGN KEY (`recorded_by`) REFERENCES `users` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payments`
--

LOCK TABLES `payments` WRITE;
/*!40000 ALTER TABLE `payments` DISABLE KEYS */;
INSERT INTO `payments` VALUES (1,3,3500.00,'2025-07-07','Cash',1),(2,1,3500.00,'2025-07-14','Cash',1),(3,2,3500.00,'2025-07-14','Cash',1),(4,4,5000.00,'2025-07-14','Cash',1),(5,5,3500.00,'2025-07-14','Cash',1),(6,6,5000.00,'2025-07-15','Cash',2),(7,7,5000.00,'2025-07-15','Cash',1),(8,9,5000.00,'2025-07-15','Cash',1),(9,8,5000.00,'2025-07-15','Cash',1),(10,10,5000.00,'2025-07-15','Cash',1),(11,13,5000.00,'2025-07-15','Cash',1),(12,11,3500.00,'2025-07-15','Cash',1),(13,12,3500.00,'2025-07-15','Cash',1),(14,10,5000.00,'2025-07-23','Cash',1);
/*!40000 ALTER TABLE `payments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rooms`
--

DROP TABLE IF EXISTS `rooms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `rooms` (
  `room_id` int(11) NOT NULL AUTO_INCREMENT,
  `dormitory_id` int(11) NOT NULL,
  `room_number` varchar(10) NOT NULL,
  `capacity` int(11) NOT NULL,
  `description` text DEFAULT NULL,
  `rent_rate` decimal(10,2) NOT NULL,
  `status_override` enum('None','Under Maintenance') NOT NULL DEFAULT 'None',
  `image_path` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`room_id`),
  KEY `dormitory_id` (`dormitory_id`),
  CONSTRAINT `rooms_ibfk_1` FOREIGN KEY (`dormitory_id`) REFERENCES `dormitories` (`dormitory_id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rooms`
--

LOCK TABLES `rooms` WRITE;
/*!40000 ALTER TABLE `rooms` DISABLE KEYS */;
INSERT INTO `rooms` VALUES (1,1,'101',2,'A classic air-conditioned room for two with single beds and a private bathroom (CR). A reliable and comfortable option.',7000.00,'None','room_101'),(2,1,'102',2,'Standard air-conditioned room with two single beds and a private bathroom (CR). A solid, comfortable choice.',7000.00,'None','room_102'),(3,1,'201',4,'Budget quad room with two sturdy bunk beds. Fan-cooled and shares a common bathroom. The most affordable option.',4500.00,'None','room_201'),(4,1,'103',4,'Budget-friendly quad room with two bunk beds. Fan-cooled, with a shared bathroom down the hall. Includes individual lockers for security.',4500.00,'None','room_103'),(5,1,'202',2,'Premium, fully air-conditioned double room with two single beds, a private bathroom, and a window facing the quiet courtyard.',8500.00,'None','room_202'),(6,1,'203',2,'Cozy air-conditioned double room with two single beds. Shares a clean, modern bathroom with one adjacent room.',7500.00,'None','room_203'),(7,1,'301',1,'Private solo room, fully air-conditioned with a single bed, dedicated study desk, and a private bathroom. Ideal for focused students.',12500.00,'None','room_301'),(8,2,'A1',2,'Modern, air-conditioned twin room with sleek furniture, built-in closets, and a shared en-suite bathroom.',8000.00,'None','room_A1'),(9,2,'A2',2,'Modern, air-conditioned twin room with a minimalist design. Clean lines and efficient use of space. Great for two friends sharing.',8000.00,'None','room_A2'),(10,2,'B1',4,'Spacious air-conditioned quad room designed for group study, featuring large desks and ample storage space.',5500.00,'None','room_B1'),(11,2,'C1',1,'Premium corner solo room, fully air-conditioned with a panoramic city view and a private bathroom. The best room in the building.',16000.00,'None','room_C1');
/*!40000 ALTER TABLE `rooms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL,
  `password_hash` varchar(255) NOT NULL,
  `first_name` varchar(50) NOT NULL,
  `last_name` varchar(50) NOT NULL,
  `email` varchar(100) NOT NULL,
  `phone_number` varchar(20) DEFAULT NULL,
  `role` enum('Main Admin','Admin','Dorm Manager','Tenant') NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `username` (`username`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'deanfx','adminpass','Dean Joefrey','Cabarles','dean.cabarles@dorm.com','09171112222','Main Admin','2025-07-07 05:36:57'),(2,'mgr_maricel','maricelpass','Maricel','Reyes','maricel.reyes@dorm.com','09182223333','Dorm Manager','2025-07-07 05:36:57'),(3,'tenant_miguel','miguelpass','Miguel','Santos','miguel.santos@email.com','09203334444','Tenant','2025-07-07 05:36:57'),(4,'tenant_lianna','liannapass','Lianna','Gomez','lianna.gomez@email.com','09214445555','Tenant','2025-07-07 05:36:57'),(6,'tenant_francine','francinepass','Francine','Terrobias','francine@dormify.com','09123456789','Tenant','2025-07-29 05:30:12'),(7,'cy','passwordsss','cyrus','pa','cyrus@gmail.com','09090909090','Tenant','2025-07-29 08:06:33');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-07-29 19:25:00
