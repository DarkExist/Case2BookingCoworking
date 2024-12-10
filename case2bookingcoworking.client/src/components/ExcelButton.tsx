import { useState } from 'react';
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';
import Modal from "react-modal"

const ExportWithFilter = () => {
    const [modalIsOpen, setModalIsOpen] = useState(false);
    const [startDate, setStartDate] = useState('');
    const [endDate, setEndDate] = useState('');
    const [audience, setAudience] = useState('');

    const handleExport = () => {
        const history = JSON.parse(localStorage.getItem('history')!) || [];

        // Фильтрация данных
        const filteredData = history.filter((item:any) => {
            const itemStartDate = new Date(item.timeOfStart);
            const itemEndDate = new Date(item.timeOfEnd);
            const isInDateRange = (!startDate || itemStartDate >= new Date(startDate)) &&
                (!endDate || itemEndDate <= new Date(endDate));
            const isAudienceMatch = audience ? item.audience === audience : true;

            return isInDateRange && isAudienceMatch;
        });

        // Формируем данные для Excel
        const worksheet = XLSX.utils.json_to_sheet(filteredData);
        const workbook = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(workbook, worksheet, 'FilteredHistory');

        // Генерируем файл Excel
        const excelBuffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
        const data = new Blob([excelBuffer], { type: 'application/octet-stream' });

        // Сохраняем файл
        saveAs(data, 'filtered_history.xlsx');

        // Закрываем модальное окно
        setModalIsOpen(false);
    };

    return (
        <div>
            <button style={{color:'white'}} onClick={() => setModalIsOpen(true)}>Export</button>

            <Modal isOpen={modalIsOpen} onRequestClose={() => setModalIsOpen(false)}>
                <h2>Export to excel</h2>
                <label>
                    TimeStart:
                    <input style={{ color: 'white' }} type="date" value={startDate} onChange={(e) => setStartDate(e.target.value)} />
                </label>
                <label>
                    TimeEnd:
                    <input style={{ color: 'white' }} type="date" value={endDate} onChange={(e) => setEndDate(e.target.value)} />
                </label>
                <label>
                    Audience:
                    <input style={{ color: 'black', backgroundColor:'white'}} type="text" value={audience} onChange={(e) => setAudience(e.target.value)} />
                </label>
                <button style={{ color: 'white' }} onClick={handleExport}>Export</button>
                <button style={{ color: 'white' }} onClick={() => setModalIsOpen(false)}>Close</button>
            </Modal>
        </div>
    );
};

export default ExportWithFilter;