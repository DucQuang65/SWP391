import React, { useEffect, useState } from "react";

const CheckInformation = ({ form, setForm, errors, validateInfo, setInfoValid, setStep }) => {
    const [cityList, setCityList] = useState([]);
    const [districtList, setDistrictList] = useState([]);
    const [wardList, setWardList] = useState([]);

    useEffect(() => {
        fetch("https://raw.githubusercontent.com/kenzouno1/DiaGioiHanhChinhVN/master/data.json")
            .then(res => res.json())
            .then(data => setCityList(data));
    }, []);

    useEffect(() => {
        if (form.province) {
            const city = cityList.find(c => c.Id === form.province);
            setDistrictList(city ? city.Districts : []);
        } else {
            setDistrictList([]);
        }
    }, [form.province, cityList]);

    useEffect(() => {
        if (form.district) {
            const district = districtList.find(d => d.Id === form.district);
            setWardList(district ? district.Wards : []);
        } else {
            setWardList([]);
        }
    }, [form.district, districtList]);

    return (
        <div className="donation-form-step step-1">
            <div className="step-header">
                <div className="user-avatar-inline">
                    <div className="avatar-circle-small">
                        <i className="bi bi-person-fill"></i>
                    </div>
                </div>
                <div className="header-content">
                    <h4 className="step-title">Kiểm tra lại thông tin cá nhân</h4>
                    <p className="step-subtitle">Vui lòng kiểm tra kỹ thông tin cá nhân trước khi tiếp tục. Nếu có sai sót, hãy chỉnh sửa tại đây.</p>
                </div>
            </div>
            <div className="info-review-form">
                <div className="form-row">
                    <div className="form-col">
                        <div className="form-group">
                            <label className="required">Số căn cước công dân</label>
                            <input
                                type="text"
                                className={`form-control${errors.documentNumber ? " is-invalid" : ""}`}
                                value={form.documentNumber}
                                onChange={e => setForm(f => ({ ...f, documentNumber: e.target.value }))}
                            />
                            {errors.documentNumber && <div className="invalid-feedback">{errors.documentNumber}</div>}
                        </div>
                    </div>
                    <div className="form-col">
                        <div className="form-group">
                            <label className="required">Tỉnh/Thành Phố</label>
                            <select
                                className={`form-select${errors.province ? " is-invalid" : ""}`}
                                value={form.province}
                                onChange={e => setForm(f => ({ ...f, province: e.target.value, district: '', ward: '' }))}
                            >
                                <option value="">Chọn tỉnh/thành phố</option>
                                {cityList.map(city => (
                                    <option key={city.Id} value={city.Id}>{city.Name}</option>
                                ))}
                            </select>
                            {errors.province && <div className="invalid-feedback">{errors.province}</div>}
                        </div>
                    </div>
                </div>
                <div className="form-row">
                    <div className="form-col">
                        <div className="form-group">
                            <label className="required">Họ và tên</label>
                            <input
                                type="text"
                                className={`form-control${errors.fullName ? " is-invalid" : ""}`}
                                value={form.fullName}
                                onChange={e => setForm(f => ({ ...f, fullName: e.target.value }))}
                            />
                            {errors.fullName && <div className="invalid-feedback">{errors.fullName}</div>}
                        </div>
                    </div>
                    <div className="form-col">
                        <div className="form-group">
                            <label className="required">Quận/Huyện</label>
                            <select
                                className={`form-select${errors.district ? " is-invalid" : ""}`}
                                value={form.district}
                                onChange={e => setForm(f => ({ ...f, district: e.target.value, ward: '' }))}
                                disabled={!form.province}
                            >
                                <option value="">Chọn quận/huyện</option>
                                {districtList.map(d => (
                                    <option key={d.Id} value={d.Id}>{d.Name}</option>
                                ))}
                            </select>
                            {errors.district && <div className="invalid-feedback">{errors.district}</div>}
                        </div>
                    </div>
                </div>
                <div className="form-row">
                    <div className="form-col">
                        <div className="form-group">
                            <label className="required">Ngày sinh</label>
                            <div className="date-input-wrapper">
                                <input
                                    type="date"
                                    className={`form-control${errors.dob ? " is-invalid" : ""}`}
                                    value={form.dob}
                                    onChange={e => setForm(f => ({ ...f, dob: e.target.value }))}
                                />
                                <i className="bi bi-calendar3 date-icon"></i>
                            </div>
                            {errors.dob && <div className="invalid-feedback">{errors.dob}</div>}
                        </div>
                    </div>
                    <div className="form-col">
                        <div className="form-group">
                            <label className="required">Phường/Xã</label>
                            <select
                                className={`form-select${errors.ward ? " is-invalid" : ""}`}
                                value={form.ward}
                                onChange={e => setForm(f => ({ ...f, ward: e.target.value }))}
                                disabled={!form.district}
                            >
                                <option value="">Chọn phường/xã</option>
                                {wardList.map(w => (
                                    <option key={w.Id} value={w.Id}>{w.Name}</option>
                                ))}
                            </select>
                            {errors.ward && <div className="invalid-feedback">{errors.ward}</div>}
                        </div>
                    </div>
                </div>
                <div className="form-row">
                    <div className="form-col">
                        <div className="form-group">
                            <label className="required">Giới tính</label>
                            <select
                                className={`form-select${errors.gender ? " is-invalid" : ""}`}
                                value={form.gender}
                                onChange={e => setForm(f => ({ ...f, gender: e.target.value }))}
                            >
                                <option value="">Chọn giới tính</option>
                                <option value="Nam">Nam</option>
                                <option value="Nữ">Nữ</option>
                            </select>
                            {errors.gender && <div className="invalid-feedback">{errors.gender}</div>}
                        </div>
                    </div>
                    <div className="form-col">
                        <div className="form-group">
                            <label className="required">Số nhà, tên đường</label>
                            <input
                                type="text"
                                className={`form-control${errors.address ? " is-invalid" : ""}`}
                                value={form.address}
                                onChange={e => setForm(f => ({ ...f, address: e.target.value }))}
                            />
                            {errors.address && <div className="invalid-feedback">{errors.address}</div>}
                        </div>
                    </div>
                </div>
                <div className="form-row">
                    <div className="form-col">
                        <div className="form-group">
                            <label>Email</label>
                            <input
                                type="email"
                                className={`form-control${errors.email ? " is-invalid" : ""}`}
                                value={form.email}
                                onChange={e => setForm(f => ({ ...f, email: e.target.value }))}
                            />
                            {errors.email && <div className="invalid-feedback">{errors.email}</div>}
                        </div>
                    </div>
                    <div className="form-col">
                        <div className="form-group">
                            <label>Số điện thoại</label>
                            <input
                                type="text"
                                className={`form-control${errors.phone ? " is-invalid" : ""}`}
                                value={form.phone}
                                onChange={e => setForm(f => ({ ...f, phone: e.target.value }))}
                            />
                            {errors.phone && <div className="invalid-feedback">{errors.phone}</div>}
                        </div>
                    </div>
                </div>
            </div>
            <div className="form-actions">
                <div></div>
                <button className="btn btn-danger" onClick={e => {
                    e.preventDefault();
                    if (validateInfo()) {
                        setInfoValid(true);
                        setStep(1);
                    }
                }}>TIẾP THEO</button>
            </div>
        </div>
    );
};
export default CheckInformation;
