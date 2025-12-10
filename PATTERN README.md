# Terminal Sapce
เป็นโปรเจ็กต์จากปี 2 โดยได้เพิ่ม pattens มา 2 อันคือ command และ state

state จะเพิ่ม boss ขึ้นมาที่มี score และ hp สูงโดยมี 3 states
NormalState - จะโจมตีใส่ผู้เล่น
CooldwonState - Boss จะเป็นวิธีการยิง
EngageState - ยิงไร้ทิษไร้ทางแบบรัวๆ

โดย state จะเป็นแบบนี้
NormalState -> CooldownState -> EngageState -> CooldownState -> NormalState -> ...

Loop ไปเรื่อยๆจงกว่า boss ตาย

command จะเพื่ม 2 power ups เมื่อ enemy ตายจะมีโอกาศดรอบ 2 power ups แบบสุ่มเพื่อสร้างความได้เปรียบ

Rapid Fire ยิงรัว 2 เท่า
Strong Bullets ค่าความเสียหาย 2 เท่า

ซึ่งจะไปเปลี่ยนค่า stat ของผู้เล่นในเวลาที่กำหนด