import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';

const ExportToExcel = () => {
    const handleExport = () => {
        // Считываем данные из localStorage
        const history = JSON.parse(localStorage.getItem('history')!) || [];

        // Формируем данные для Excel
        const worksheet = XLSX.utils.json_to_sheet(history);
        const workbook = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(workbook, worksheet, 'History');

        // Генерируем файл Excel
        const excelBuffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
        const data = new Blob([excelBuffer], { type: 'application/octet-stream' });

        // Сохраняем файл
        saveAs(data, 'history.xlsx');
    };

    return (
        <div>
            <button style={{color:"white"}} className="excelButton" onClick={handleExport}>Export</button>
        </div>
    );
};

export default ExportToExcel;