void setup(){ 
  Serial.begin(9600);  // set baud rate 

  pinMode(6, INPUT);
  pinMode(7, INPUT);

  // set default to be internal pullup resistor enabled 
  digitalWrite(6, HIGH);
  digitalWrite(7, HIGH);
}

void loop() { 
  if(digitalRead(6) == LOW) {    
    Serial.write(2); 
    Serial.flush(); 
    delay(10);
  } 
  else if (digitalRead(7) == LOW){ 
    Serial.write(1);
    Serial.flush();
    delay(10);
  }
 
  
}
