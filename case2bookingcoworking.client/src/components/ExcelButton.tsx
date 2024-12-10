import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';

const ExportToExcel = () => {
    const handleExport = () => {
        // ��������� ������ �� localStorage
        const history = JSON.parse(localStorage.getItem('history')!) || [];

        // ��������� ������ ��� Excel
        const worksheet = XLSX.utils.json_to_sheet(history);
        const workbook = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(workbook, worksheet, 'History');

        // ���������� ���� Excel
        const excelBuffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
        const data = new Blob([excelBuffer], { type: 'application/octet-stream' });

        // ��������� ����
        saveAs(data, 'history.xlsx');
    };

    return (
        <div>
            <button style={{color:"white"}} className="excelButton" onClick={handleExport}>Export</button>
        </div>
    );
};

export default ExportToExcel;