using TMPro;

namespace Tools {
	public static class TMP_DropdownFix {
		public static void SetValueAndCallback(this TMP_Dropdown dropdown, int index){
			var isSame = dropdown.value == index;
			if (isSame){
				dropdown.onValueChanged.Invoke(index);
			} else{
				dropdown.value = index;
			}
		}
	}
}