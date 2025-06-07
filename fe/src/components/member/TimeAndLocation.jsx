import React from "react";
const TimeAndLocation = ({ setStep }) => (
    <div className="donation-form-step">
        <div className="form-group">
            <label className="required">Chọn ngày hiến máu</label>
            <div className="date-input-wrapper">
                <input type="date" className="form-control" />
                <i className="bi bi-calendar3 date-icon"></i>
            </div>
        </div>
        <div className="form-group">
            <label>Địa điểm hiến máu</label>
            <div className="location-info-box">
                <div className="info-item">
                    <i className="bi bi-telephone-fill info-icon"></i>
                    <div className="info-content">(028) 3957 1343</div>
                </div>
                <div className="info-item">
                    <i className="bi bi-envelope-fill info-icon"></i>
                    <div className="info-content">anhduonghospital@gmail.com</div>
                </div>
                <div className="info-item">
                    <i className="bi bi-clock-fill info-icon"></i>
                    <div className="info-content">
                        <div><strong>Thứ 2 – Chủ nhật:</strong></div>
                        <div className="schedule">
                            <div className="schedule-line">• Sáng: 07:00 – 12:00</div>
                            <div className="schedule-line">• Chiều: 13:00 – 16:30</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div className="form-group working-hours-input">
            <label>Khung giờ làm việc của địa điểm</label>
            <select className="form-select">
                <option value="">Chọn khung giờ</option>
                <option value="7:00-11:00">Sáng: 07:00 – 11:00</option>
                <option value="13:00-17:00">Chiều: 13:00 – 17:00</option>
            </select>
        </div>
        <div className="form-actions">
            <button className="btn btn-outline-secondary" onClick={e => { e.preventDefault(); setStep(0); }}>QUAY VỀ</button>
            <button className="btn btn-danger" type="button" onClick={e => { e.preventDefault(); setStep(2); }}>TIẾP THEO</button>
        </div>
    </div>
);
export default TimeAndLocation;
