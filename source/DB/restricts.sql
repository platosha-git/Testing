/*ALTER TABLE Tour
ADD CONSTRAINT FK_TF FOREIGN KEY (Food) REFERENCES Food(FoodID),
ADD CONSTRAINT FK_TH FOREIGN KEY (Hotel) REFERENCES Hotel(HotelID),
ADD CONSTRAINT FK_TT FOREIGN KEY (Transfer) REFERENCES Transfer(TransferID);
*/

/*CHECK*/
/*ALTER TABLE Food
ADD CONSTRAINT FCost_CHK CHECK (Cost > 0),
ADD CONSTRAINT FCat_CHK CHECK (Category = 'Breakfast' OR Category = 'Half board' OR 
							   Category = 'Full board' OR Category = 'All inclusive' OR 
							   Category = 'Continental breakfast' OR Category = 'American breakfast'),
ADD CONSTRAINT FMenu_CHK CHECK (Menu = '' OR Menu = 'Vegeterian' OR Menu = 'Children' OR Menu = 'Dietary');

ALTER TABLE Tour 
ADD CONSTRAINT TCost_CHK CHECK (Cost > 0),
ADD CONSTRAINT TDate_CHK CHECK (DateBegin < DateEnd);

ALTER TABLE Users
ADD CONSTRAINT UAL_CHK CHECK (AccessLevel >= 0 AND AccessLevel <= 2);

ALTER TABLE Hotel 
ADD CONSTRAINT HCost_CHK CHECK (Cost > 0),
ADD CONSTRAINT HClass_CHK CHECK (Class >= 0 AND Class <= 5),
ADD CONSTRAINT HType_CHK CHECK (Type = 'Hotel' OR Type = 'Apartment' OR Type = 'Hostel' OR 
								Type = 'Guest house' OR Type = 'Motel' OR Type = 'Vila' OR 
								Type = 'Camping' OR Type = 'BnB');

ALTER TABLE Transfer
ADD CONSTRAINT TCost_CHK CHECK (Cost > 0),
ADD CONSTRAINT TType_CHK CHECK (Type = 'Bus' OR Type = 'Plane' OR Type = 'Train');
*/