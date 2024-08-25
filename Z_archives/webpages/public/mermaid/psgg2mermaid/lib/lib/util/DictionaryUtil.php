<?php
/**
 * Generated by Haxe 4.1.3
 */

namespace lib\util;

use \php\Boot;
use \system\collections\generic\Dictionary;

class DictionaryUtil {
	/**
	 * @param Dictionary $dic
	 * 
	 * @return Dictionary
	 */
	public static function Clone ($dic) {
		#src/lib/util/DictionaryUtil.hx:38: characters 9-114
		$dic2 = new Dictionary();
		#src/lib/util/DictionaryUtil.hx:39: lines 39-42
		$_g = 0;
		$_g1 = $dic->GetEnumerator();
		while ($_g < $_g1->length) {
			#src/lib/util/DictionaryUtil.hx:39: characters 14-15
			$p = ($_g1->arr[$_g] ?? null);
			#src/lib/util/DictionaryUtil.hx:39: lines 39-42
			++$_g;
			#src/lib/util/DictionaryUtil.hx:41: characters 13-37
			$dic2->Add($p->Key, $p->Value);
		}
		#src/lib/util/DictionaryUtil.hx:43: characters 9-20
		return $dic2;
	}

	/**
	 * @param Dictionary $dic
	 * @param mixed $key
	 * 
	 * @return mixed
	 */
	public static function Get ($dic, $key) {
		#src/lib/util/DictionaryUtil.hx:10: lines 10-13
		if (($dic !== null) && $dic->ContainsKey($key)) {
			#src/lib/util/DictionaryUtil.hx:12: characters 13-42
			return $dic->GetValue_TKey($key);
		}
		#src/lib/util/DictionaryUtil.hx:14: characters 9-20
		return null;
	}

	/**
	 * @param Dictionary $dic
	 * @param mixed $key
	 * 
	 * @return bool
	 */
	public static function Remove ($dic, $key) {
		#src/lib/util/DictionaryUtil.hx:29: lines 29-33
		if ($dic->ContainsKey($key)) {
			#src/lib/util/DictionaryUtil.hx:31: characters 13-28
			$dic->Remove($key);
			#src/lib/util/DictionaryUtil.hx:32: characters 13-24
			return true;
		}
		#src/lib/util/DictionaryUtil.hx:34: characters 9-21
		return false;
	}

	/**
	 * @param Dictionary $dic
	 * @param mixed $key
	 * @param mixed $val
	 * 
	 * @return void
	 */
	public static function Set ($dic, $key, $val) {
		#src/lib/util/DictionaryUtil.hx:18: lines 18-25
		if ($dic->ContainsKey($key)) {
			#src/lib/util/DictionaryUtil.hx:20: characters 13-40
			$dic->SetValue_TKey($key, $val);
		} else {
			#src/lib/util/DictionaryUtil.hx:24: characters 13-30
			$dic->Add($key, $val);
		}
	}

	/**
	 * @return void
	 */
	public function __construct () {
	}
}

Boot::registerClass(DictionaryUtil::class, 'lib.util.DictionaryUtil');
