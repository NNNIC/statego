var G = function () {};

// statemachines
G.main_control = null; // main stata machine
G.fade_control = null; // fade state machine
G.fadetxt_control = null; // fade text state machine
G.fe_control = null; // front end state machine
G.key_control = null; // key state machine
G.create_panel_control = null; // crate panel state machine
G.map_control = null; // map control
G.touch_control = null; // touch control
G.move_logic_control = null; // move logic control
G.title_control = null; // title control

// rendere params
G.datapath = './data/';//location.protocol + '//' + location.host + '/data/';
G.mouse     = new THREE.Vector2();
G.raycaster = new THREE.Raycaster();
G.mouse_click = false;   //一回のTick内有効
G.mouse_down = false;    //マウスボタンダウン中
G.key_down = '';


G.width  = 0;
G.height = 0;

G.loader = null;
G.renderer = null;
G.scene  = null;
G.camera = null;
G.light = null;

G._2d_scene = null;
G._2d_camera = null;

G.fade_scene = null;
G.fade_camera = null;

G.title_scene = null;
G.title_camera = null;

G.time_diff = 0;

// update  list
G.update_list = [];
function add_update(sm) {
    G.update_list.push(sm);
}
function remove_update(sm) {
    var index = G.update_list.findIndex(sm);
    if (index >= 0) {
        G.update_list.slice(index,1);
        console.log('Removed : ' + sm);
    }
}
function exec_update () {
    for(var i=0; i< G.update_list.length; i++) {
        var sm = G.update_list[i];
        if (sm !== null) {
            sm.update();
        }
    }
}

// 
G.mapinfo = [];
G.mondai = [];

// move obj in scene
function move_obj(name,x,y) {
    var obj = find_obj_in_scene(name);
//    for(var i = 0; i<G.scene.children.length; i++)
//    {
//        var t = G.scene.children[i];
//        if (t.name === name)
//        {
//            obj = t;
//            break;
//        }
//    }
    if (obj===null) return;
    var z = obj.position.z;
    obj.position.set(x,y,z);
}
function copyToClipboard(str) {
  const el = document.createElement('textarea');
  el.value = str;
  document.body.appendChild(el);
  el.select();
  document.execCommand('copy');
  document.body.removeChild(el);
}

function find_obj_in_scene(name) {
    var obj = null;
    for(var i = 0; i<G.scene.children.length; i++)
    {
        var t = G.scene.children[i];
        if (t.name === name)
        {
            obj = t;
            break;
        }
    }
    return obj;    
}
function find_obj_in_2dscene(name) {
    var obj = null;
    for(var i = 0; i<G._2d_scene.children.length; i++)
    {
        var t = G._2d_scene.children[i];
        if (t.name === name)
        {
            obj = t;
            break;
        }
    }
    return obj;    
}
function show_mondai(n) {
    var num = n % 4;
    var mondai = G.mondai[ num ];
    for(var i = 0; i<16; i++)
    {
        var panel = mondai[i];
        var n = 'n' +(i+1);
        var info = G.mapinfo[n];
        info.panel = panel;
        move_obj(panel,info.x,info.y);
    }    
}
function zeroPadding(num,length){ //https://qiita.com/terrym/items/6257f6507ca19f00cdf3
    return ('0000000000' + num).slice(-length);
}
function is_smartphone() {
    return false;
    
    var ua = navigator.userAgent;
    if (ua.indexOf('iPhone') > 0)   return true;
    if (ua.indexOf('iPad') > 0)     return true;    
    if (ua.indexOf('iPod') > 0)     return true;
    if (ua.indexOf('Android') > 0)  return true;
    
    return false;
}