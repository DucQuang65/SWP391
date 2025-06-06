import React, { useState } from "react";
import GuestHomePage from "../guest/GuestHomePage";
import NavbarBase from "../../components/common/NavbarBase";
import "../../styles/pages/MemberHomePage.scss";

const navItems = [
  { path: "/", label: "Trang chủ" },
  { path: "/blood-info", label: "Tài liệu" },
  { path: "/blog", label: "Tin tức" },
  { path: "/donation-guide", label: "Hướng dẫn hiến máu" },
];

const MemberNavbar = () => {
  const [showMenu, setShowMenu] = useState(false);
  const userInfo = {
    name: "Nguyễn Văn A",
    avatar: "", // Có thể thay bằng link ảnh nếu có
  };
  const actionItems = [];
  return (
    <>
      <NavbarBase
        // logoSrc={blood1}
        // logoAlt="Blood Donation Logo"
        navItems={navItems}
        actionItems={actionItems}
        userInfo={{
          ...userInfo,
          avatar: (
            <div
              className="member-avatar-wrapper"
              onClick={() => setShowMenu((prev) => !prev)}
            >
              {userInfo.avatar || userInfo.name.charAt(0).toUpperCase()}
            </div>
          ),
        }}
      />
      {showMenu && (
        <div className="member-dropdown-menu">
          <a href="/notifications">Thông báo cá nhân</a>
          <a href="/profile">Hồ sơ cá nhân</a>
        </div>
      )}
    </>
  );
};

const MemberHomePage = () => {
  return <GuestHomePage CustomNavbar={MemberNavbar} />;
};

export default MemberHomePage;
