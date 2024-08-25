<?php
/**
 * Generated by Haxe 4.1.3
 */

namespace lib\util;

use \system\text\Encoding;
use \php\Boot;
use \system\Cs2Hx;
use \system\collections\generic\Dictionary;
use \psgg\HxFile;
use \php\_Boot\HxString;
use \system\Console;
use \psgg\HxEncoding;

class MacroUtil {
	/**
	 * @var Dictionary
	 */
	public $m_default_macro_ht;
	/**
	 * @var Dictionary
	 */
	public $m_gensrc_macro_ht;
	/**
	 * @var Dictionary
	 */
	public $m_macro_ini_ht;

	/**
	 * @return void
	 */
	public function __construct () {
	}

	/**
	 * @param string $key
	 * 
	 * @return string
	 */
	public function GetValue ($key) {
		#src/lib/util/MacroUtil.hx:73: lines 73-88
		$_gthis = $this;
		#src/lib/util/MacroUtil.hx:75: lines 75-78
		if (($this->m_gensrc_macro_ht !== null) && $this->m_gensrc_macro_ht->ContainsKey($key)) {
			#src/lib/util/MacroUtil.hx:77: characters 13-85
			return $_gthis->m_gensrc_macro_ht->GetValue_TKey($key);
		}
		#src/lib/util/MacroUtil.hx:79: lines 79-82
		if (($this->m_macro_ini_ht !== null) && $this->m_macro_ini_ht->ContainsKey($key)) {
			#src/lib/util/MacroUtil.hx:81: characters 13-82
			return $_gthis->m_macro_ini_ht->GetValue_TKey($key);
		}
		#src/lib/util/MacroUtil.hx:83: lines 83-86
		if (($this->m_default_macro_ht !== null) && $this->m_default_macro_ht->ContainsKey($key)) {
			#src/lib/util/MacroUtil.hx:85: characters 13-86
			return $_gthis->m_default_macro_ht->GetValue_TKey($key);
		}
		#src/lib/util/MacroUtil.hx:87: characters 9-20
		return null;
	}

	/**
	 * @param PsggDataFileUtil_Item $psggItem
	 * @param string $doc_path
	 * 
	 * @return void
	 */
	public function ReadAllMacroSettings ($psggItem, $doc_path) {
		#src/lib/util/MacroUtil.hx:13: characters 9-163
		$filename = $psggItem->get_setting_String_String("setting", "macro_ini");
		#src/lib/util/MacroUtil.hx:14: lines 14-26
		if (!(($filename === null) || (mb_strlen($filename) === 0))) {
			#src/lib/util/MacroUtil.hx:16: lines 16-25
			try {
				#src/lib/util/MacroUtil.hx:18: characters 17-109
				$path = HxFile::Combine_String_String($psggItem->GetIncDir($doc_path), $filename);
				#src/lib/util/MacroUtil.hx:19: characters 17-108
				$text = HxFile::ReadAllText_String_Encoding($path, Encoding::get_UTF8());
				#src/lib/util/MacroUtil.hx:20: characters 17-72
				$this->m_macro_ini_ht = IniUtil::CreateHashtable($text);
			} catch(\Throwable $_g) {
				#src/lib/util/MacroUtil.hx:24: characters 17-144
				Console::WriteLine("{4F39CB16-1508-444A-A57B-63961C3ABFE4}\x0AFile Cannot read :" . ((($filename === null ? "" : $filename))??'null') . ":");
			}
		}
		#src/lib/util/MacroUtil.hx:27: characters 9-172
		$this->m_default_macro_ht = $psggItem->get_setting("macro");
		#src/lib/util/MacroUtil.hx:28: lines 28-71
		try {
			#src/lib/util/MacroUtil.hx:30: characters 13-42
			$macroinitext = "";
			#src/lib/util/MacroUtil.hx:31: characters 13-69
			$path = $psggItem->GetGeneratedSource($doc_path);
			#src/lib/util/MacroUtil.hx:32: characters 44-92
			$str = $psggItem->GetSrcEnc();
			#src/lib/util/MacroUtil.hx:32: characters 13-180
			$enc = (($str === null) || (mb_strlen($str) === 0) ? Encoding::get_UTF8() : HxEncoding::GetEncoding_String($psggItem->GetSrcEnc()));
			#src/lib/util/MacroUtil.hx:33: characters 13-82
			$text = HxFile::ReadAllText_String_Encoding($path, $enc);
			#src/lib/util/MacroUtil.hx:34: characters 13-72
			$lines = Cs2Hx::Split($text, \Array_hx::wrap([10]));
			#src/lib/util/MacroUtil.hx:35: characters 13-39
			$bInMacro = false;
			#src/lib/util/MacroUtil.hx:36: lines 36-58
			$_g = 0;
			while ($_g < $lines->length) {
				#src/lib/util/MacroUtil.hx:36: characters 18-19
				$l = ($lines->arr[$_g] ?? null);
				#src/lib/util/MacroUtil.hx:36: lines 36-58
				++$_g;
				#src/lib/util/MacroUtil.hx:38: lines 38-53
				if (!$bInMacro) {
					#src/lib/util/MacroUtil.hx:40: lines 40-44
					if (HxString::indexOf($l, ":psgg-macro-start") !== -1) {
						#src/lib/util/MacroUtil.hx:42: characters 25-40
						$bInMacro = true;
						#src/lib/util/MacroUtil.hx:43: characters 25-33
						continue;
					}
				} else if (HxString::indexOf($l, ":psgg-macro-end") !== -1) {
					#src/lib/util/MacroUtil.hx:50: characters 25-41
					$bInMacro = false;
					#src/lib/util/MacroUtil.hx:51: characters 25-33
					continue;
				}
				#src/lib/util/MacroUtil.hx:54: lines 54-57
				if ($bInMacro) {
					#src/lib/util/MacroUtil.hx:56: characters 21-115
					$macroinitext = ($macroinitext??'null') . ((($l === null ? "" : $l))??'null') . "\x0A";
				}
			}
			#src/lib/util/MacroUtil.hx:59: lines 59-66
			if ($bInMacro) {
				#src/lib/util/MacroUtil.hx:61: characters 130-191
				$str = $psggItem->GetGeneratedSourceFileName();
				#src/lib/util/MacroUtil.hx:61: characters 17-192
				Console::WriteLine("Cannot find " . ":psgg-macro-end" . " in " . ((($str === null ? "" : $str))??'null'));
			} else {
				#src/lib/util/MacroUtil.hx:65: characters 17-83
				$this->m_gensrc_macro_ht = IniUtil::CreateHashtable($macroinitext);
			}
		} catch(\Throwable $_g) {
			#src/lib/util/MacroUtil.hx:70: characters 101-162
			$str = $psggItem->GetGeneratedSourceFileName();
			#src/lib/util/MacroUtil.hx:70: characters 13-163
			Console::WriteLine("{28F2C476-7C8B-4C75-8115-A5E543DABAB1}\x0AFile Cannot read :" . ((($str === null ? "" : $str))??'null'));
		}
	}
}

Boot::registerClass(MacroUtil::class, 'lib.util.MacroUtil');
