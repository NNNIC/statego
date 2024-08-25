// -- application
G = new GControl(); //グローバル設定
G.run();

// 
var sm = new MainControl();
sm.start();

const rootobj = document.getElementById('root');
const logobj  = document.getElementById('log');
var counter = 0;

function step() {
  if (!sm.is_end()) {
    sm.update();
  }
  //rootobj.innerHTML = counter++;
  window.requestAnimationFrame(step);
}
window.requestAnimationFrame(step);
//
