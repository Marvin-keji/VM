
'*******************************************************ECAT���߳�ʼ��
'''''''''

global CONST BUS_TYPE = 0				 '�������͡�������λ�����ֵ�ǰģʽ
global CONST MAX_AXISNUM = 4			 '�������
global CONST Bus_Slot	= 0				'��λ��0
global CONST Bus_AxisStart	 = 0		'��������ʼ���

global Bus_InitStatus			'���߳�ʼ�����״̬
Bus_InitStatus = -1
global  Bus_TotalAxisnum		'���ɨ���������

delay(8000)				'��ʱ3S�ȴ��������ϵ�
'**********************��ʼ��ECAT���� 
Ecat_Init()

end

global sub Ecat_Init()
	for i=0 to MAX_AXISNUM - 1		'��ʼ����ԭ������					
		AXIS_ENABLE(i) = 0
		atype(i)=0	
	next
		
	Bus_InitStatus = -1
	Bus_TotalAxisnum = 0	
	SLOT_STOP(Bus_Slot)
	delay(200)
	slot_scan(Bus_Slot)											'��ʼɨ��
	if return then 
		
		?"����ɨ��ɹ�","�����豸����"NODE_COUNT(Bus_Slot)
		?
		?"��ʼӳ�����"
		for i=0 to NODE_COUNT(Bus_Slot)-1						'�������������д�վ�ڵ�

			if NODE_AXIS_COUNT(Bus_Slot,i) <>0 then			'�жϵ�ǰ�ڵ��Ƿ��е��
				for j=0 to NODE_AXIS_COUNT(Bus_Slot,i)-1
					AXIS_ADDRESS(Bus_AxisStart+i)=Bus_TotalAxisnum+1			'ӳ�����
					ATYPE(Bus_AxisStart+i)=65									'���ÿ���ģʽ 65-λ�� 66-�ٶ� 67-ת�� 
					DRIVE_PROFILE(Bus_AxisStart+i)= 4							'����PROFILE����
					disable_group(Bus_AxisStart+i)								'ÿ�ᵥ������

					DRIVE_IO(Bus_AxisStart+i) = 128 + (Bus_AxisStart+i)*16		'ӳ���������ϵ�IO״̬
					REV_IN(Bus_AxisStart+i) = 128 + (Bus_AxisStart+i)*16 
					FWD_IN(Bus_AxisStart+i) = 129 + (Bus_AxisStart+i)*16
					DATUM_IN(Bus_AxisStart+i) = 130 + (Bus_AxisStart+i)*16
					INVERT_IN(128 + (Bus_AxisStart+i)*16,ON)
					INVERT_IN(129 + (Bus_AxisStart+i)*16,ON)
					INVERT_IN(130 + (Bus_AxisStart+i)*16,ON)
					
					Bus_TotalAxisnum=Bus_TotalAxisnum+1							'������+1
				next
			endif
			
		next
		
		?"���ӳ�����","������������"Bus_TotalAxisnum
		wa 2000
		
		SLOT_START(Bus_Slot)						'��������
		if return then 
			?"���߿����ɹ�"
			
			?"��ʼ�������������(���������������ֵ�����)"
			for i= Bus_AxisStart to Bus_AxisStart + Bus_TotalAxisnum - 1 
				
				DRIVE_CONTROLWORD(i)=128					'���������������ֵ�
				wa 100
				DRIVE_CONTROLWORD(i)=6
				wa 100
				DRIVE_CONTROLWORD(i)=15
				wa 100
			next
			
			?"����������������"
			wa 100

			?"�������������"
			datum(0)
			DRIVE_CLEAR(0)
			?"����������������"
			wa 100
						
			?"��ʹ��׼��"
			for i= Bus_AxisStart to Bus_AxisStart + Bus_TotalAxisnum - 1
				base(i)
				AXIS_ENABLE=1
			next
			wdog=1											'ʹ���ܿ���
			Bus_InitStatus  = 1
			?"��ʹ�����"
		else
			?"���߿���ʧ��"
			Bus_InitStatus = 0
		endif
		
	else
		?"����ɨ��ʧ��"
		Bus_InitStatus = 0
	endif
end sub