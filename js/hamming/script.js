var Point = function(x, y){
    this.x = x;
    this.y = y;
   };
   Point.prototype.move = function(deltaX, deltaY){
    this.x += deltaX;
    this.y += deltaY;
   };
   // Наследование
   var ColorPoint = function(x, y, color){
    Point.call(this, x, y);
    this.color= color;
   }
   ColorPoint.prototype = Object.create(Point.prototype);
   ColorPoint.prototype.constructor = ColorPoint;
   // Инстанцирование
   var myPoint = new Point(1, -1);
   var myColorPoint = new ColorPoint(4, 3,
   'red');
   myColorPoint.move(2,4);
   console.log(myColorPoint.x, myColorPoint.y)
   // 6, 7
