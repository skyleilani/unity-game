const int pin6_Button = 6; 
const int pin7_Button = 7; 

void setup(){ 
  Serial.begin(9600);  // set baud rate 

  pinMode(pin6_Button, INPUT);
  pinMode(pin7_Button, INPUT);

  digitalWrite(pin6_Button, HIGH);
  digitalWrite(pin7_Button, HIGH);
}

void loop() { 
  if(digitalRead(pin6_Button) == LOW) {    
    Serial.write(2); // binary instead of ASCII
    Serial.flush(); 
    delay(10);
  } 
  else if (digitalRead(pin7_Button) == LOW) { 
    Serial.write(1);
    Serial.flush();
    delay(10);
  }
}