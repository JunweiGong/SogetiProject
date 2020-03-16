set identity_insert house off;

INSERT INTO House(ID, name, owner, description) VALUES('1422' ,'house1', 'Tom', 't');
set identity_insert imageDo on;
INSERT INTO imageDo(imgId, stage, houseId, imgRef,imgRefThumbNail) VALUES('1','1','1422' , 'null','null');
INSERT INTO imageDo(imgId, stage, houseId, imgRef,imgRefThumbNail) VALUES('1','2','1422' , 'null','null');
set identity_insert stageDo off;
INSERT INTO stageDo(stageId, houseId, stage) VALUES('2' , '1422','2');

