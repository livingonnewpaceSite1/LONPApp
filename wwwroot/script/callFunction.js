console.log("Hello World!")
function autoTab(obj, value) {
	//debugger;
	/* ��˹��ٻẺ��ͤ�������� _ ᷹������á��� ���ǵ����������ͧ����
	�����ѭ�ѡɳ������� �蹡�˹���  �ٻẺ�Ţ���ѵû�ЪҪ�
	4-2215-54125-6-12 ������ö��˹���  _-____-_____-_-__
	�ٻẺ�������Ѿ�� 08-4521-6521 ��˹��� __-____-____
	���͡�˹������� 12:45:30 ��˹��� __:__:__
	������ҧ��ҧ��ҧ�繡�á�˹��ٻẺ�Ţ�ѵû�ЪҪ�
	*/
	if (event.keyCode < 48 || event.keyCode > 57) { event.returnValue = false }

	switch (value) {
		case 1:
			var pattern = new String("_-____-_____-__-_"); // ��˹��ٻẺ㹹��
			break;
		case 2:
			var pattern = new String("__-____-____"); // ��˹��ٻẺ㹹��
			break;
		case 3:
			var pattern = new String("____-______-_"); // ��˹��ٻẺ㹹��
			break;
	}

	var pattern_ex = new String("-"); // ��˹��ѭ�ѡɳ���������ͧ���·������㹹��
	var returnText = new String("");
	var obj_l = obj.value.length;
	var obj_l2 = obj_l - 1;
	for (i = 0; i < pattern.length; i++) {
		if (obj_l2 == i && pattern.charAt(i + 1) == pattern_ex) {
			returnText += obj.value + pattern_ex;
			obj.value = returnText;
		}
	}
	if (obj_l >= pattern.length) {
		obj.value = obj.value.substr(0, pattern.length);
	}
}
function HC_out(obj, callback) {
	if (obj.HC.value != '') {
		if (obj.HC.value.length != 13) {
			alert("��سҡ�͡ ���ʺ�ҹ ���١��ͧ")
			obj.HC.focus();
		} else {
			var script = document.createElement('script');
			script.src = 'search_hc.php?hc=' + obj.HC.value;
			document.head.appendChild(script);
		}

	}
}
function SaveClick(obj) {

	if (parseInt(obj.HHM.value) < parseInt(obj.PPP.value)) { obj.HHM.value = obj.PPP.value }

	if (obj.HC.value.length != 13) {
		alert('��سҡ�͡���ʺ�ҹ���ú 11 ���')
		obj.HC.focus();
	} else if (obj.PP.value == '') {
		alert('��سҡ�͡ �ӹǹ����������ԧ')
		obj.PP.focus();
	} else {
		obj.next.value = 1
		obj.submit()
	}
}

function NextClick(obj) {
	if (confirm('��ҹ����ͧ��úѹ�֡���������������')) {
		obj.next.value = 0
		obj.submit();
	}
}